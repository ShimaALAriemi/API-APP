namespace APIapp.Model
{
    public class ToDo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Createddate { get; set; } = DateTime.Now;
        public bool isFinished { get; set; }

    }
}
