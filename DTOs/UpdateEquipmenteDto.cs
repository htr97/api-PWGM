using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UpdateEquipmenteDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string SystemType { get; set; }
        public string StorageType { get; set; }
        public string StorageCap { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string OsName { get; set; }
        public string Observation { get; set; }
        public int UbicationId { get; set; }
    }
}