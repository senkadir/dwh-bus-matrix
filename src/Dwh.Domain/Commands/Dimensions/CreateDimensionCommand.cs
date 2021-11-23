namespace Dwh.Domain.Commands
{
    public class CreateDimensionCommand
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}
