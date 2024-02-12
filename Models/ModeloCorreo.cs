namespace ms_notificaciones_torneos.Models
{
    public class ModeloCorreo
    {
        public string? correoDestino { get; set; } 
        public string? nombreDestino { get; set; }
        public string? asuntoCorreo { get; set; }
        public string? contenidoCorreo { get; set; }
        public string? codigo2fa { get; set; }
        public string? hash { get; set; }
    }
}