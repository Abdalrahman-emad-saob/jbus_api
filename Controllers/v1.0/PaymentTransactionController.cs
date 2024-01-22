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

        [HttpPost]
        public ActionResult<string> GenerateQrCode(PaymentTransactionCreateDto paymentTransactionCreateDto)
        {
            string data = $"{paymentTransactionCreateDto.DriverId},{paymentTransactionCreateDto.BusId},{paymentTransactionCreateDto.RouteId},{DateTime.UtcNow}";

            string signature = GenerateSignature(data);

            string combinedData = $"{data},{signature}";

            string encryptedData = Encrypt(combinedData);

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(encryptedData, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(10);
            string base64String = Convert.ToBase64String(qrCodeAsPngByteArr);
            return Ok(base64String);
        }
        private string DecryptQrCode(string encryptedData)
        {
            string decryptedData = _cryptoService.Decrypt(encryptedData);
            return decryptedData;
        }

        private string GenerateSignature(string inputText)
        {
            string encryptedText = _cryptoService.Encrypt(inputText);
            return encryptedText;
        }

        private string Encrypt(string plainText)
        {
            string encryptedText = _cryptoService.Encrypt(plainText);
            return encryptedText;
        }

    }
}