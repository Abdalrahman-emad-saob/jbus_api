using API.Validations;
using Microsoft.AspNetCore.Mvc;
using QRCoder;


namespace API.Controllers.v1
{
    // [CustomAuthorize("PASSENGER", "DRIVER")]
    public class PaymentTransactionController : BaseApiController
    {


        // public class QrCodeService
        // {
            [HttpGet]
            public ActionResult<byte[]> GenerateQrCode(string inputText)
            {
                // TODO - Payload
                using MemoryStream ms = new();
                QRCodeGenerator oQRCodeGenerator = new();
                QRCodeData oQRCodeData = oQRCodeGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.L);
                BitmapByteQRCode qrCode = new(oQRCodeData);
                string base64string = Convert.ToBase64String(qrCode.GetGraphic(20));
                return Ok(qrCode.GetGraphic(5));
            }
        // }

    }
}