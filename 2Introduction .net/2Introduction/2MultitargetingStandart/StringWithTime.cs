using System;


namespace _2MultitargetingStandart
{
    public class StringWithTime
    {
        DateTime dateTime { get; set; }

        public string OutputNameDatetime(string name)
        {
            dateTime = DateTime.Now;
            string output = $"{dateTime} Hello,{name}!";
            return output;
        }
    }
}
