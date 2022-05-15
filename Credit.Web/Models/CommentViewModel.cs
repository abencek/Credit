using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Credit.Web.Models
{
    public class CommentViewModel
    {
        [DisplayName ("Comment ID")]
        public	int	CommentId { get; set; }

        [DisplayName ("Comment type")]
        public	string	CommentType	{ get; set; }

        [DisplayName ("Creation date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public	DateTime CommentDate { get; set; }

        [DisplayName ("Description")]
        public	string CommentDescription { get; set; }
    }   
}
