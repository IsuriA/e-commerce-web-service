namespace e_commerce_web.core.DTOs
{
    public class LookupDto: ILookupDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
