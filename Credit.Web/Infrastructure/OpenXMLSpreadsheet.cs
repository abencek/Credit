using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Globalization;
using System.Text.RegularExpressions;



namespace Credit.Web.Infrastructure
{   
    /// <summary>
    /// OpenXML library for creating xlsx files
    /// </summary>
    
    public class OpenXMLSpreadsheet
    {

        private const int DEFAULT_COLUMN_WIDTH = 15;   

        public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
        {
            DataSet ds = new();
            ds.Tables.Add(DataTransform.ListToDataTable(list));

            return CreateExcelDocument(ds, xlsxFilePath);
        }


        public static bool CreateExcelDocument(DataSet ds, string excelFilename)
        {
            try
            {
                using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(ds, spreadsheet);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static MemoryStream CreateExcelDocumentStream<T> (List<T> list)
        {
            DataSet ds = new();
            ds.Tables.Add(DataTransform.ListToDataTable(list));

            MemoryStream stream = new();
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
            {
                WriteExcelFile(ds, document);
            }
            stream.Position=0;
            return stream;
        }

       
        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
        {
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new Workbook();

            WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            workbookStylesPart.Stylesheet = GenerateStyleSheet();
            workbookStylesPart.Stylesheet.Save();

            uint worksheetNumber = 1;
            Sheets sheets = spreadsheet.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            foreach (DataTable dt in ds.Tables)
            {
                string worksheetName = dt.TableName;

                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                Sheet sheet = new() { 
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart), 
                    SheetId = worksheetNumber, 
                    Name = worksheetName 
                };

                sheets.Append(sheet);

                WriteDataTableToExcelWorksheet(dt, newWorksheetPart);

                worksheetNumber ++;
            }

            spreadsheet.WorkbookPart.Workbook.Save();
        }

        private static Stylesheet GenerateStyleSheet()
        {
            uint iExcelIndex = 164;

            return new Stylesheet(
                new NumberingFormats( 
                    new NumberingFormat ()                                                  // Custom number format # 164: especially for date-times
                    {
                        NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++),
                        FormatCode = StringValue.FromString("dd/MMM/yyyy hh:mm:ss") 
                    },
                    new NumberingFormat()                                                   // Custom number format # 165: especially for date times (with a blank time)
                    {
                        NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++),
                        FormatCode = StringValue.FromString("dd/MMM/yyyy")
                    }
               ),
                new Fonts(
                    new Font(                                                               // Index 0 - The default font.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),
                    new Font(                                                               // Index 1 - A bold font.
                        new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),
                    new Font(                                                               // Index 2 - An Italic font.
                        new Italic(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri"/*"Times New Roman"*/ })
                ),
                new Fills(
                    new Fill(                                                           // Index 0 - The default fill.
                        new PatternFill() { PatternType = PatternValues.None }),
                    new Fill(                                                           // Index 1 - The default fill of gray 125 (required)
                        new PatternFill() { PatternType = PatternValues.Gray125 }),
                    new Fill(                                                           // Index 2 - The light gray fill.
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFD9D9D9"} }
                        ) { PatternType = PatternValues.Solid }),
                    new Fill(                                                           // Index 3 - The dark-gray fill.
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FF404040" } }
                        ) { PatternType = PatternValues.Solid })
                ),
                new Borders(
                    new Border(                                                         // Index 0 - The default border.
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()),
                    new Border(                                                         // Index 1 - Applies a Left, Right, Top, Bottom border to a cell
                        new LeftBorder( new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder( new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder( new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder( new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                ),
                new CellFormats(
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },                         // Style # 0 - The default cell style.  If a cell does not have a style index applied it will use this style combination instead
                    new CellFormat() { NumberFormatId = 164 },                                         // Style # 1 - DateTimes
                    new CellFormat() { NumberFormatId = 14 },                                          // Style # 2 - Default Excel Date (with a blank time)
                    new CellFormat( 
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center } )
                        { FontId = 1, FillId = 3, BorderId = 0, ApplyFont = true, ApplyAlignment = true },     // Style # 3 - Header row 
                    new CellFormat() { NumberFormatId = 3 },                                                   // Style # 4 - Number format: #,##0
                    new CellFormat() { NumberFormatId = 4 },                                                   // Style # 5 - Number format: #,##0.00
                    new CellFormat() { FontId = 1, FillId = 0, BorderId = 0, ApplyFont = true },               // Style # 6 - Bold 
                    new CellFormat() { FontId = 2, FillId = 0, BorderId = 0, ApplyFont = true },               // Style # 7 - Italic
                    new CellFormat() { FontId = 2, FillId = 0, BorderId = 0, ApplyFont = true },               // Style # 8 - Times Roman
                    new CellFormat() { FontId = 1, FillId = 2, BorderId = 0, ApplyFill = true },               // Style # 9 - Light gray Fill
                    new CellFormat(                                                                            // Style # 10 - Alignment
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    ) { FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true },
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }              // Style # 11 - Border
                )
            ); // return
        }

        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart)
        {
            OpenXmlWriter writer = OpenXmlWriter.Create(worksheetPart, Encoding.ASCII);
            writer.WriteStartElement(new Worksheet());

            UInt32 inx = 1;
            writer.WriteStartElement(new Columns());
            foreach (DataColumn dc in dt.Columns)
            {
                writer.WriteElement(new Column { Min = inx, Max = inx, CustomWidth = true, Width = DEFAULT_COLUMN_WIDTH });
                inx ++;
            }
            writer.WriteEndElement();


            writer.WriteStartElement(new SheetData());
            string cellValue;
            string cellReference;


            int numberOfColumns = dt.Columns.Count;
            bool[] IsIntegerColumn = new bool[numberOfColumns];
            bool[] IsFloatColumn = new bool[numberOfColumns];
            bool[] IsDateColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //  Create the Header row
            uint rowIndex = 1;
            writer.WriteStartElement(new Row { RowIndex = rowIndex, /*Height = 20, CustomHeight = true*/ });
            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];
                AppendHeaderTextCell(excelColumnNames[colInx] + "1", col.ColumnName, writer);
                IsIntegerColumn[colInx] = (col.DataType.FullName.StartsWith("System.Int"));
                IsFloatColumn[colInx] = (col.DataType.FullName == "System.Decimal") || 
                    (col.DataType.FullName == "System.Double") || 
                    (col.DataType.FullName == "System.Single");
                IsDateColumn[colInx] = (col.DataType.FullName == "System.DateTime");
            }
            writer.WriteEndElement();   

            //  Create data rows
            double cellFloatValue;
            CultureInfo ci = new("en-US");
            foreach (DataRow dr in dt.Rows)
            {
                ++rowIndex;

                writer.WriteStartElement(new Row { RowIndex = rowIndex });

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();
                    cellValue = ReplaceHexadecimalSymbols(cellValue);
                    cellReference = excelColumnNames[colInx] + rowIndex.ToString();

                    // Create cell with data
                    if (IsIntegerColumn[colInx] || IsFloatColumn[colInx])
                    {
                        cellFloatValue = 0;
                        if (double.TryParse(cellValue, out cellFloatValue))
                        {
                            cellValue = cellFloatValue.ToString(ci);
                            AppendNumericCell(cellReference, cellValue, writer);
                        }
                    }
                    else if (IsDateColumn[colInx])
                    {
                        if (DateTime.TryParse(cellValue, out DateTime dateValue))
                        {
                            AppendDateCell(cellReference, dateValue, writer);
                        }
                        else
                        {
                            AppendTextCell(cellReference, cellValue, writer);
                        }
                    }
                    else
                    {
                        //  For text cells
                        AppendTextCell(cellReference, cellValue, writer);
                    }
                }
                writer.WriteEndElement(); //  End of Row
            }
            writer.WriteEndElement();     //  End of SheetData
            writer.WriteEndElement();     //  End of worksheet

            writer.Close();
        }

        private static void AppendHeaderTextCell(string cellReference, string cellStringValue, OpenXmlWriter writer)
        {
            writer.WriteElement(new Cell
            {
                CellValue = new CellValue(cellStringValue),
                CellReference = cellReference,
                DataType = CellValues.String,
                StyleIndex = 6
            });
        }

        private static void AppendTextCell(string cellReference, string cellStringValue, OpenXmlWriter writer)
        {
            writer.WriteElement(new Cell 
            { 
                CellValue = new CellValue(cellStringValue), 
                CellReference = cellReference, 
                DataType = CellValues.String 
            });
        }

        private static void AppendDateCell(string cellReference, DateTime dateTimeValue, OpenXmlWriter writer)
        {
            string cellStringValue = dateTimeValue.ToOADate().ToString(CultureInfo.InvariantCulture);
            bool bHasBlankTime = (dateTimeValue.Date == dateTimeValue);

            writer.WriteElement(new Cell
            {
                CellValue = new CellValue(cellStringValue),
                CellReference = cellReference,
                StyleIndex = UInt32Value.FromUInt32(bHasBlankTime ? (uint)2 : (uint)1),
                DataType = CellValues.Number        
            });
        }

        private static void AppendNumericCell(string cellReference, string cellStringValue, OpenXmlWriter writer)
        {
            //Style #4 formats with 0 decimal places, style #5 formats with 2 decimal places
            UInt32 cellStyle = 0U;
 
            writer.WriteElement(new Cell 
            { 
                CellValue = new CellValue(cellStringValue), 
                CellReference = cellReference,
                StyleIndex = cellStyle,         
                DataType = CellValues.Number 
            });
        }

        private static string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }

        //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Z, AA, AB, AC... AY, AZ, BA, BB..)
        private static string GetExcelColumnName(int columnIndex)
        {

            int firstInt = columnIndex / 676;
            int secondInt = (columnIndex % 676) / 26;
            if (secondInt == 0)
            {
                secondInt = 26;
                firstInt--;
            }
            int thirdInt = (columnIndex % 26);

            char firstChar = (char)('A' + firstInt - 1);
            char secondChar = (char)('A' + secondInt - 1);
            char thirdChar = (char)('A' + thirdInt);

            if (columnIndex < 26)
                return thirdChar.ToString();

            if (columnIndex < 702)
                return string.Format("{0}{1}", secondChar, thirdChar);

            return string.Format("{0}{1}{2}", firstChar, secondChar, thirdChar);
        }


        private static class DataTransform
        {
            public static DataTable ListToDataTable<T>(List<T> list)
            {
                DataTable dt = new();

                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
                }
                foreach (T t in list)
                {
                    DataRow row = dt.NewRow();
                    foreach (PropertyInfo info in typeof(T).GetProperties())
                    {
                        if (!IsNullableType(info.PropertyType))
                            row[info.Name] = info.GetValue(t, null);
                        else
                            row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                    }
                    dt.Rows.Add(row);
                }
                return dt;
            }

            private static Type GetNullableType(Type t)
            {
                Type returnType = t;
                if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    returnType = Nullable.GetUnderlyingType(t);
                }
                return returnType;
            }

            private static bool IsNullableType(Type type)
            {
                return (type == typeof(string) ||
                        type.IsArray ||
                        (type.IsGenericType &&
                        type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
            }
        }

    }

}
