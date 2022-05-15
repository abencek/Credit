  
  //Editable table plugin
  function PluginEditable(defaultDropdowns) {
    var pluginName = "editable"
    var defaults = {
        button: true,
        buttonAdd: ".add",
        buttonEdit: ".edit",
        buttonDeleteCancel: ".delete",
        maintainWidth: true,
        dropdowns: defaultDropdowns || {},
        setButtons:function(){},
        edit: function() {},
        save: function() {},
        cancel: function() {}
    };

    function editable(element, options) {
      this.element = element;
      this.options = $.extend({}, defaults, options);

      this._defaults = defaults;
      this._name = pluginName;

      this.init();
    }

    editable.prototype = {
        init: function() {
            this.editing = false;
            if (this.options.button) {
                $(this.options.buttonEdit, this.element).bind('click', this.toggle.bind(this));
                $(this.options.buttonDeleteCancel, this.element).bind('click', this.delete.bind(this));
                $(".add", this.element).bind('click', this.add.bind(this));
            }
        },

        add: function(e) {
            e.preventDefault();
            //Create and add new row to the table
            var newRow = $(this.element).first().clone(false);
            newRow.find(".d-none").removeClass("d-none");
            newRow.find(".add").closest("div").empty();
            $(this.element).first().before(newRow);
            newRow.editable();
            //Display row in edit mode
            newRow.find(".edit").click();
        },


        toggle: function(e) {
            e.preventDefault();
            this.editing = !this.editing;
            if (this.editing) {
                this.edit();
            } else {
                this.save(e);
            }
        },

        edit: function() {
            var instance = this,
                values = {};

            $('td[data-field]', this.element).each(function() {
                var input,
                    field = $(this).data('field'),
                    edit = $(this).data('edit'),
                    value = $(this).text(),
                    width = $(this).width();
                values[field] = value;
                $(this).empty();

                if (instance.options.maintainWidth) {
                    $(this).width(width);
                }

                if (field in instance.options.dropdowns) {
                    input = $('<select></select>');
                    for (var i = 0; i < instance.options.dropdowns[field].length; i++) {
                        $('<option></option>')
                        .text(instance.options.dropdowns[field][i])
                        .appendTo(input);
                    };
                    input.attr('name',field).val(value).data('old-value', value);
                } else {
                    input = $('<input type="text" />')
                            .attr('name',field)
                            .attr("readonly",function(){if(edit){return "readonly"}})
                            .val(value)
                            .data('old-value', value);
                }

                input.appendTo(this);
            });
            this.options.edit.bind(this.element)(values);
            this.setButtons();
        },

        save: function() {
            var instance = this,
                values = {};

            var resultDone = function (){
                $('td[data-field]', this.element).each(function() {
                    var value = $(':input', this).val();
                    values[$(this).data('field')] = value;
                    $(this).empty().text(value);
                });
                if (instance.options.maintainWidth) {
                    $(this).width("auto");
                }
                this.options.save.bind(this.element)(values);
                this.setButtons();
            };

            var resultError=function () {
                alert("Error - data can not be updated!");
            };

            var isNameFieldEmpty = !$(':input', this.element).first().val();

            var isNameDuplicity = !!$('tr td:first-child', $(instance.element).closest("tbody"))
                .map(function(){
                    if($(this).text().toUpperCase() == $(':input', instance.element).first().val().toUpperCase()) {
                        return $(this).text();
                    }
                }).length

            // If wrong data, display error, otherwise save
            if (isNameFieldEmpty || isNameDuplicity) {
                resultError();
                instance.cancel();
            }
            else {  
                if (this.isChanged()) {
                    var sendData = $("td[data-field]", this.element).find(":input").serialize();
                    $.ajax(
                        {
                            url: $(".edit", instance.element).data("action"),
                            method: "POST",
                            data: sendData
                        })
                        .done(function () { resultDone.call(instance); })
                        .fail(function () {
                            resultError();
                            instance.cancel();
                        });
                }
                else {
                    resultDone.call(instance);
                }
            }

        },

        cancel: function() {
            var instance = this;
            var values = {};

            if (instance.options.maintainWidth) {
                $(this).width("auto");
            }
            $('td[data-field]', this.element).each(function() {
                var value = $(':input', this).data('old-value');
                values[$(this).data('field')] = value;
                $(this).empty().text(value);
            });
            this.options.cancel.bind(this.element)(values);
            this.setButtons();
        },

        delete: function() {
            var instance = this;
            var resultDone = function (){
                $(this.element).remove();
            };
            var resultError=function () {
                alert("Error - data can not be updated!");
            };

            if (this.editing) {
                this.editing = !this.editing;
                this.cancel();
            } else {
                var name=$(':first', this.element).text();
                if(name){
                    var sendData="Name="+name;
                    $.ajax(
                        {
                            url:$(".delete",instance.element).data("action"),
                            method: "DELETE",
                            data:sendData
                        })
                        .done(function(){resultDone.call(instance)})
                        .fail(function(){resultError()});
                }
                else{
                    resultError();
                }
            }
            
        },

        isChanged: function(){  
            var changed=false;
            $('td[data-field]', this.element).each(function() {
                var oldVal=$(':input', this).data('old-value');
                var newVal=$(':input', this).val();
                if(oldVal!==newVal) {
                    changed=true;
                    return false;
                };
            });
            return changed;
        },

        setButtons: function() {
            $(".edit", this.element).text(function(index,text){if(text.trim()=='Save') {return "Edit"} else {return "Save"}})
                .attr('title', function(){if(this.title=='Save'){return 'Edit'}else{return 'Save'}}
            );
            $(".delete", this.element).text(function(index,text){
                if(text.trim()=='Delete') {return "Cancel"} else {return "Delete"}})
                .attr('title', function(){
                    if(this.title=='Delete') {return 'Cancel'} else {return 'Delete'}}
            );
        },

        _captureEvent: function(e) {
            e.stopPropagation();
        },

    };

    $.fn[pluginName] = function(options) {
      return this.each(function() {
        if (!$.data(this, "plugin_" + pluginName)) {
          $.data(this, "plugin_" + pluginName,
            new editable(this, options));
        }
      });
    };
  }


// Load and configure plugin   
$(document).ready(function () {
    PluginEditable(tableDropdowns||{});
    $('table tr').editable({
        edit: function() {},
        save: function() {},
        cancel: function() {}
    });
});



