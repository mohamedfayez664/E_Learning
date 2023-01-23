namespace DAL.DTO
{
    public class ResponseDto<T>
    {
        public List<T> Response { get; set; }
        public int PageCount { get; set; }

        //ResponseDto<T> GetAllResponseByFilter<Tin>(Tin filter)
        //{
        //    return ResponseDto;
        //}
    }
}
