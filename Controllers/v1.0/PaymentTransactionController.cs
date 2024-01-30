using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;
using QRCoder;


namespace API.Controllers.v1
{

    public class PaymentTransactionController(
        IPaymentTransactionRepository _paymentTransactionRepository,
        IDriverRepository driverRepository,
        IPassengerRepository passengerRepository,
        ICryptoService cryptoService,
        ITokenHandlerService tokenHandlerService,
        ITripRepository tripRepository
        ) : BaseApiController
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository = _paymentTransactionRepository;
        private readonly IDriverRepository _driverRepository = driverRepository;
        private readonly IPassengerRepository _passengerRepository = passengerRepository;
        private readonly ICryptoService _cryptoService = cryptoService;
        private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
        private readonly ITripRepository _tripRepository = tripRepository;

        [CustomAuthorize("DRIVER")]
        [HttpPost("generateQrCode")]
        public async Task<ActionResult> GenerateQrCode()
        {
            int id = _tokenHandlerService.TokenHandler();
            var driver = await _driverRepository.GetDriverById(id);
            if (driver == null)
                return BadRequest(new { Error = "Driver" });
            if (driver.Bus == null)
                return BadRequest(new { Error = "Bus" });
            if (driver.Bus.Route == null)
                return BadRequest(new { Error = "Route" });
            if (driver.DriverTrips == null)
                return BadRequest(new { Error = "DriverTrip" });
            var driverTrip = driver.DriverTrips.Find(dt => dt.status == Status.ONGOING || dt.status == Status.PENDING);
            if (driverTrip == null)
                return BadRequest(new { Error = "DriverTrip2" });

            string data = $"{id},{driverTrip.Id},{driver.BusId},{driver.Bus.RouteId},{driver.Bus.Route.Fee},{DateTime.UtcNow}";

            string signature = GenerateSignature(data);

            string combinedData = $"{data},{signature}";

            string encryptedData = Encrypt(combinedData);

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(encryptedData, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(10);
            string base64String = Convert.ToBase64String(qrCodeAsPngByteArr);
            return Ok(new { base64String });
        }
        [CustomAuthorize("PASSENGER")]
        [HttpPost("scanQrCode")]
        public async Task<ActionResult<string>> ScanQrCode(EncryptedDataDto encryptedData)
        {
            string decryptedData = DecryptQrCode(encryptedData.EncryptedData!);
            string[] data = decryptedData.Split(',');
            string signature = data[^1];
            string combinedData = string.Join(',', data[0..^1]);
            string decryptedSignature = _cryptoService.Decrypt(signature);
            if (decryptedSignature == combinedData)
            {

                int id = _tokenHandlerService.TokenHandler();
                var passenger = await _passengerRepository.GetPassengerById(id);
                var trip = passenger!.Trips.Find(t => t.status == TripStatus.ONGOING || t.status == TripStatus.PENDING);

                if (trip == null)
                    return BadRequest(new { Error = "Trip" });

                var PaymentTransactionCreateDto = new PaymentTransactionCreateDto
                {
                    PassengerId = id,
                    TripId = trip.Id,
                    DriverId = int.Parse(data[0]),
                    BusId = int.Parse(data[2]),
                    RouteId = int.Parse(data[3]),
                    Amount = double.Parse(data[4]),
                    TimeStamp = DateTime.UtcNow
                };
                var pt = await _paymentTransactionRepository.CreatePaymentTransaction(PaymentTransactionCreateDto);
                if (pt == null)
                    return StatusCode(500, new { Error = "Error in Creating PaymentTransaction" });
                await _tripRepository.Update(new TripUpdateDto() { Status = TripStatus.ONGOING.ToString(), PaymentTransactionId = pt.Id }, trip.Id);
                try
                {
                    if (await _paymentTransactionRepository.SaveChanges())
                        return Ok(new { Success = "Valid and Trip Status Updated}"});
                }
                catch (Exception)
                {
                    return BadRequest(new { Error = "Duplciated Payment for the trip" });
                }

            }
            return BadRequest(new { Error = "Invalid" });
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