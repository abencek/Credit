using System;
using Newtonsoft.Json;


namespace Credit.Data.App.Models
{   
    public class Comment
    {
        public	int	CommentId { get; set; }

        public	CommentType	CommentType { get; set; }

        public	DateTime CommentDate { get; set; }

        public	string CommentDescription { get; set; }

        [JsonIgnore]
        public virtual	Customer Customer  { get; set; }
    }
}
