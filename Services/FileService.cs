namespace DondeComemos.Services
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile file, string folder);
        bool DeleteImage(string imageUrl);
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            // Validar extensión
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Tipo de archivo no permitido");

            // Validar tamaño (5MB máximo)
            if (file.Length > 5 * 1024 * 1024)
                throw new InvalidOperationException("El archivo es demasiado grande (máximo 5MB)");

            // Crear nombre único
            var fileName = $"{Guid.NewGuid()}{extension}";
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads", folder);
            
            // Crear carpeta si no existe
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            // Guardar archivo
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folder}/{fileName}";
        }

        public bool DeleteImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl) || imageUrl.StartsWith("http"))
                return false;

            try
            {
                var filePath = Path.Combine(_environment.WebRootPath, imageUrl.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
            }
            catch
            {
                // Log error si es necesario
            }

            return false;
        }
    }
}