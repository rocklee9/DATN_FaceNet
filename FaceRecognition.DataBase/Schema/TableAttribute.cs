using System;

namespace FaceRecognition.DataBase.Schema
{
    internal class TableAttribute : Attribute
    {
        private string v;

        public TableAttribute(string v)
        {
            this.v = v;
        }
    }
}