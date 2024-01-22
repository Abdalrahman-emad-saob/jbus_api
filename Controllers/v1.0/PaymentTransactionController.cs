using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;
using QRCoder;


namespace API.Controllers.v1
{
    // [CustomAuthorize("PASSENGER", "DRIVER")]
    public class PaymentTransactionController : BaseApiController
    {
        private readonly ICryptoService _cryptoService;

        public PaymentTransactionController(
            ICryptoService cryptoService
        )
        {
            _cryptoService = cryptoService;
        }

        [HttpGet]
        public ActionResult<string> GenerateQrCode(PaymentTransactionCreateDto paymentTransactionCreateDto)
        {
            Guid guid = Guid.NewGuid();
            string data = $"{guid},{paymentTransactionCreateDto.DriverId},{paymentTransactionCreateDto.BusId},{paymentTransactionCreateDto.RouteId},{DateTime.UtcNow}";

            string signature = GenerateSignature(data);

            string combinedData = $"{data},{signature}";

            string encryptedData = Encrypt(combinedData);

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(encryptedData, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qrCode = new(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(10);
            string base64String = Convert.ToBase64String(qrCodeAsPngByteArr);
            return Ok(signature);
        }

        private string GenerateSignature(string inputText)
        {
            string encryptedText = _cryptoService.Encrypt(inputText);
            return encryptedText;
        }

        private string Encrypt(string encryptedText)
        {
            string decryptedText = _cryptoService.Decrypt(encryptedText);
            return decryptedText;
        }

    }
}