﻿

namespace dapperTask1.Models
{
    internal class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }=[];
    }
}
