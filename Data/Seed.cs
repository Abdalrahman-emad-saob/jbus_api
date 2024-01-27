using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class Seed
    {
        public static async Task SeedPassenger(DataContext context)
        {
            if (await context.Passengers.AnyAsync()) return;
            var passwordHasher = new PasswordHasher<User>();

            User user = new()
            {
                Name = "Abood Saob",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                PhoneNumber = "0785455414",
                Email = "aboodsaob1139@gmail.com",
                Role = Role.PASSENGER,
                Sex = Sex.MALE,
                DateOfBirth = new DateOnly(2002, 3, 26),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            Passenger passenger = new()
            {
                Wallet = 1000,
                GoogleToken = "google_token_here",
                FacebookToken = "facebook_token_here",
                User = user
            };
            user.Passenger = passenger;
            context.Users.Add(user);
            context.Passengers.Add(passenger);
            await context.SaveChangesAsync();
        }

        public static async Task SeedAdmin(DataContext context)
        {
            if (await context.Admins.AnyAsync()) return;
            var passwordHasher = new PasswordHasher<User>();

            User user = new()
            {
                Name = "Fadl Al masri",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                PhoneNumber = "0791012625",
                Email = "fdlmasri@gmail.com",
                Role = Role.SUPER_ADMIN,
                Sex = Sex.MALE,
                DateOfBirth = new DateOnly(2002, 6, 25),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            Admin admin = new()
            {
                User = user,
            };
            user.Admin = admin;
            context.Users.Add(user);
            context.Admins.Add(admin);
            await context.SaveChangesAsync();
        }

        public static async Task SeedDriver(DataContext context)
        {
            if (await context.Drivers.AnyAsync()) return;
            var passwordHasher = new PasswordHasher<User>();

            User user = new()
            {
                Name = "Khader Abumallouh",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                PhoneNumber = "0790364258",
                Email = "khader.mallouh@gmail.com",
                Role = Role.DRIVER,
                Sex = Sex.MALE,
                DateOfBirth = new DateOnly(2000, 6, 25),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            Driver driver = new()
            {
                User = user,
            };
            user.Driver = driver;
            context.Users.Add(user);
            context.Drivers.Add(driver);
            await context.SaveChangesAsync();
        }

        public static async Task SeedPoint(DataContext context)
        {
            if (await context.Points.AnyAsync()) return;
            Point point1 = new()
            {
                Name = "JUST Bus Station",
                Latitude = 32.49512209286742,
                Longitude = 35.98597417188871,
                CreatedAt = DateTime.UtcNow,
            };
            context.Points.Add(point1);
            Point point2 = new()
            {
                Name = "North Bus Station",
                Latitude = 31.9957018434082,
                Longitude = 35.91987132195803,
                CreatedAt = DateTime.UtcNow,
            };
            Point point3 = new()
            {
                Name = "حي الجامعة الاردنية",
                Latitude = 32.02511248566629,
                Longitude = 35.89201395463377,
                CreatedAt = DateTime.UtcNow,
            };
            context.Points.Add(point1);
            context.Points.Add(point2);
            context.Points.Add(point3);
            await context.SaveChangesAsync();
        }
        public static async Task SeedInterestPoints(DataContext context)
        {
            if (await context.InterestPoints.AnyAsync()) return;
            var point1 = context.Points.Find(1);
            var point2 = context.Points.Find(2);

            InterestPoint startingPoint = new()
            {
                Name = "North Bus Station",
                Logo = "5.png",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            InterestPoint endingPoint = new()
            {
                Name = "Ending Point",
                Logo = "5.png",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            startingPoint.LocationId = point2!.Id;
            endingPoint.LocationId = point1!.Id;
            context.InterestPoints.Add(startingPoint);
            context.InterestPoints.Add(endingPoint);

            await context.SaveChangesAsync();
        }

        public static async Task SeedRoute(DataContext context)
        {
            if (await context.Routes.AnyAsync()) return;
            var interestpoint1 = context.InterestPoints.Find(1);
            var interestpoint2 = context.InterestPoints.Find(2);

            Entities.Route route = new()
            {
                Name = "Amman-JUST",
                WaypointsGoing = @"iahbEorfzEg@FYJUTS\Qf@Kl@EHETQVQJe@FQJyDJuBNc@?{@JwAf@qAb@m@NkBZs@D_A@cBGm@Io@OqBw@i@UkMqEcD}@oB]kBMeDGaBDkEf@cJdBsC|@q@^qAfAaApAe@bAk@vAu@hCuCbPgF`XwB|L_CvLgFvZ[tAQfBkBpK{AtE}AtC}B~BmDzBoDrBs@d@{FdDsErCo@TuAT}A@sCOmLkC{DcAoCo@{E{@yD_@cBIuCAa@BSDsAJyB\qA\{CzAmAr@mAbAgBpBwA|Bc@x@y@tByAfF}B~IuAtGe@|CuA|FkAnF{BrJ{AlH}@rFwDbVm@~B_A`C_AjBkAjB_AfA_Ax@y@b@oAj@_ARgAJu@BYDoA@sJW}CDyGVoJn@qD\_H~@}CRwCDqEGeB@wDPiF^_E\_GZeABuAAqCSoB]sBi@qAg@i@WeCaB}AwAsLyLwKgL_FgEe@e@e@w@gBeBaBmAyAs@_AY}@QgBWwAEqAAaKTy@?cAGoC]wBk@}@_@kAu@}BaBcA{@m@s@m@{@_A_BaA{B{@aCwB_HaBuF{CmJ{EgPm@mBg@wAo@{AqAcCmBqCc@i@wBoBwCwBwJeGqPcJwBw@yA[iAK_AA}@BoANcB`@eBr@qAt@{@r@sEdFo@v@gAlBo@`Bc@|A[bBmAhK[tAs@|AgAvAe@`@q@d@{@`@mAb@}QvCiCXsHlAmEl@}Cn@eGdA{C^uCl@wAh@]PiAbASV{@pAc@dAo@dCM|@KfB?fADfAHv@b@fDTfCAlCM|A]|Ai@tA{@rA[`@yBxBkAdAu@t@aClCsAfBu@nAi@|AOj@Q`AMfAEbAApADvBR`Bb@|AZbAjN~YjBdEnErJ|@pBbEtIdOv[~CdH`ArBr@jCNjA@tAEl@UrA[x@O\w@bAy@h@a@Ny@NqA@qCUcSkHcB_@wB_@iBSyBQwISsCKyAS{Ag@qDcBeA]q@OuAOcQDcHCmAFwAXwAZyDhAmAXsARyYbDqAH_D@wAGqC_@_OiDoG}AwCm@mHiBoCm@iEgAy@Mm@CkABw@Jw@Py@Xu@^uAlA_AjAaA`C]lBgBxOSbB]hBs@xBa@~@o@jAaAtAuApAk@j@mAt@o@ZuAf@_ATeBVu@HkBAiBKm@KwDcAwKeDkBc@qEg@mBCgBDkBPeC\oBd@mD|A{Ax@{AfAmDrCaBjAy@`@u@Xm@Py@LyBHeAEsASuGyAgNqDeQaEu@W[OoAw@{@y@i@m@kAmBo@}AwDuNi@mAm@y@m@o@cAo@eAa@wAS_A?cAJw@Tc@Rm@`@wCvCk@r@gFfFqMdM}@t@u@\}@X[F}@Du@Aw@O}@Y{@i@iAeAwK_LmAuAkAiBa@y@o@aB_BkFgBgFgAuBmA}AkB{Bm@iAe@qA_@gBS{BAcA@i@h@aJ?uAMcBOw@W{@o@uA[g@g@k@eA{@_Aa@cAW}@K{MaAgFc@oJo@eFc@mB_@mA_@gCeA}ByA}BoBuBiCiBuCqMyTyAqBgAiAk@_@{@c@aAa@kB_@oAG{@BiAH{AT_Gp@kDVgACeASk@Mu@a@YQq@o@w@iAWi@k@mB{AyGeA_EeDcNiAcFKmACeABoAdBmRPaBF{@?}AAk@KcBU{AOu@]s@yAsBq@q@eAy@aCkAkA[gDe@{JuA_B_@sAc@oBaAwBoA}AoA_EuDcMcMwAsAwA_A_Ae@_Aa@iBa@kBSmCE{BNwATwKhC_[tH{JxB{G`AeQrBgBNiDJeCBoCIyDMmBMkEe@kEu@oM}CgMaDwCw@gHgBiA_@wAu@eAs@gAiAmAiBo@sAc@mAi@iC_AcGQ}@}@aC{@_B}@iAaNkOeG_HsIiJoUyWqBeBuAw@}Am@uA[gBUmIe@{FUiRi@iCKeBMiJQ}FSkOYyECwQNuKFeIJ}BCgCEoCMmIo@aDa@cDg@uCi@oLuCwAS{@GcBCwAFeBVgATuKvDsBn@sAV_CTmA@aCK]EaCm@sB}@eBkAuPmM}@m@_EoBcCw@}Ba@qBWgHq@_SqB{KaAqB?sBN{ATeCx@oAl@gCfBaLfJoBvA_@Tu@Z{Bt@gEfA}IdBkF|@}HzAuHjBsLbC}IbBeALkBHsACsBSq@MkBg@_I{DmBu@yAWs@GqAIiADqALqATcE`A_BTgBDkAAcAIoB_@_Bq@o@[w@i@uBsBgNuOqAiAsA_Aw@c@eAc@eDq@wBMwE@gAC_BSoA_@_Ai@u@k@}@mAq@sAk@mBKc@mAiI]qAy@kBy@oAm@m@u@k@}YiRkHqEgRyLuBkBuK_LiA}Aq@oAi@eAc@iAu@mCi@aDS_CgB{e@KaBQyAUmAm@oBe@iA_A_B{@iAm@m@}@u@}A_AyBy@cASmBQ}B?iBP_Ch@yPpFmATcBL{AE}@K{A_@oAe@cAi@o@e@}AaBsGmJy@iAaAcAwAmAoDyBeE}BaA_@yA[uAGw@@y@HsAXYHaAd@gAv@}A|AoArAaAv@qAv@sBz@wPpFoBf@mCb@mAN_F^kELaD@cBE_CSmHkAsNmCmCc@qFiAaEe@yBOqIC_XPwLB_JI_DWeDi@}VcHsYiI_Cc@_C_@kIqBskAu\eHsBcBa@kAUcL}Aui@kGuBc@aBg@_Bo@qXyMkHgDuEeBc[aKsCcAkBw@eG_DaYuOy\qRoBuA_RqNwAcA{BiBmDmCkIgGmPaMkAaAyAyAI_@A_@HYxDiIPAPYAB",
                WaypointsReturning = @"euieEinszEMRQ@eBpDEOwBcB_GiEFUEUOKS@MNAT@B[x@W`@e@f@IJS@_@\e@fAAPD\RTTDT?jF|C\VpDrBfAr@hAj@~NxKvB~AdTlPxGdFxJjHrA|@`]vRb]fRdClAvBx@nKnDfQtFtEdBdIvDhVnL~Ar@dC|@r@TzEt@z]jErN~AzCf@pCj@vElA`q@nRjXtH|SzFpTjGjGjB`LzCpDfAvA`@pCd@dAN|CTjCFlr@_@pC@`EVbALlJdBxRrD~F~@xBP`DHzBAzDQfGc@bAKpCk@bBc@vHaCnHeCnAi@|BcBdAeAhBmB|@o@tAq@d@OrAUh@Cj@A`BLp@Nz@Zv@^hGlDz@j@~AtA`@b@zDnFtAtBfC`DbBvAvAr@xAd@`BZbBJlAGp@I|@OdK}CnFeBtA[hBO~A?vAJv@LdAVl@RnAl@dBlA`A|@|@jA^l@`@x@l@`B`@`BRvAPzBvAza@RtDH`Ah@bD`@|Ap@rBj@rAj@bAdBbCbHlHpBtBlEdDtWlPtUjOnBjArA|@~@z@h@r@`@n@f@jAV`Aj@rDRdBZ`BNl@l@`Bn@nAfAlApA~@x@\`B`@lANz@BjGFv@HhBXbAX`A`@~@f@hAv@~AvAfFpFzHjIr@l@x@j@|At@nBj@t@L~@Fv@@~@ArAM|@MvEgApAWjBMlAB`AJnAP`Bj@|GhDtAn@zBh@~@L~BLtBEz@G|Ba@dFaAxAUlJoBzF}A~O}CrFaA~FoAjCq@vAc@~B_ArA_AnHaGfFgEbBaAlAg@dA[zB[l@ItBEpAH|TpBzLtA~Ed@xCf@vA^r@VfCjAdCzA|QnNpAv@hBp@hCj@b@HhAHxA?lBOdBYjA[~EgB`FmBpBc@|ASpBAdBFdC`@lItBdE|@lDj@jJ`ApEXdDFpD@pEC|FIbC@`SQxD?lEHbC@vCL|OXzBH`BNnHRtDNdEF`@Bd@HxB@hBJvDLfH`@rAR|A`@vAj@jAn@dA~@b@`@hE`FpJpKtEbFx^pa@bAvAbAnBVp@`@xAb@xB`AbG\nAp@zA|@~AbAlAxAnAz@f@vBx@xPrExHdBxBl@fHfB~E`AxAVlJt@dDD~GI|BKlHw@jM}AnDk@zK_Cnc@wKtDs@hCQ~A@|AJhCf@pAd@hBbAhBzAzSxStAjAnC|A~Ap@vBp@t@PbEn@dBR|G`An@RfBx@`@Vv@p@vAjBbAzB\vARxBDlA@n@MxB{@dJo@~GK|ABlBRvAnD|N`@nBzBbJdAvEj@~Ah@bA\b@r@p@h@^bAb@pAVp@B|@?hDYtEk@dAQxBW`@?lBH|@LjA^tAr@`Ar@j@h@~A~BhCtEfJrOn@~@jB`ChAhAzAnAxA~@lB`ArBr@fB^hCXtJx@hPpA|Hh@hAT|@^z@l@jAzAJRh@rARv@TnB?rAOrC]pEA~@HlBFf@`@xB^jAXn@v@rA`CrC^f@z@~Al@xAzD~Lb@dAlA|BjArA`MdMt@r@d@^vAl@~@PfAFd@C`AOhAa@b@YfB}AvIiIlBmBlAkArE}E|A}AxA}@v@UlAKhABl@J~@\r@^~@x@x@hAf@tAtC|KZfAl@tAbA~Ar@~@bA|@n@f@nAh@rA^dOjDpTrFdB`@vARbDElAQf@KjAc@f@UhDgCjAcAlCoBjAo@rB{@`Bk@jB_@fAO~BO`BC~BBzALpB\hAV~JvCtDbAnATr@JpADn@?pCSjASnBm@|BiAh@[t@o@z@eAv@gAn@eAz@iBXw@n@gCRmA`B{NVoBVgARo@\y@\q@d@i@v@u@z@i@`Bm@v@MrAGn@@r@DbCh@|LzCdLjCpNhDhDj@fBRv@DjBB|BKjFg@xPmBpBYdDu@dG_Bv@M|FExT?z@Dv@Lz@T`EjBvB|@j@LjCZpAH`FLnCRhItA`Dp@xBh@`IpCX`@hBlA~@t@p@t@hAxAbAjAx@v@b@Vb@LfAFP?hAi@VS\a@Pq@NgA@_AEaAu@oHq@kFWcAi@yAcDyGkRqa@yC{GiGsM}CiHwE}JcGeMq@eBg@eCKu@I}A?sAJaBNeATgARm@n@wA`AyAlEaFdFeF`AuA\o@l@gBXqANsBAwAGwAw@_HGuA?{@ToCRuALe@h@qA^q@v@cA|@o@`Ag@n@QrB[nE{@hEs@|A]~Ck@lC]jDm@vDk@vAMbEm@t@OlEo@pAUzA_@lAc@~A_Ar@s@b@k@|@aBd@wALw@t@sGXwBRgAt@eCz@mBr@iA~@mAzCeDt@m@pAy@z@e@fBm@v@Sh@I|AIdAB^BvARx@R~@^rEdC`Bz@vLbH|GbEpA~@rAfArA~A^h@bBtCbAvBf@nA~Lha@dCtIdAzCbApC|AhDt@jAv@bAlAhArCvBvAv@rAj@bB^jCXdCBjCCfBDhCCpA?|@FbAJtBh@lAb@pBhA`CxBnAnARLvDhDnClCdEjEnFtFpEtE`BxAnAlAv@j@dC|AbCv@dAVlBX`BLxCF`CMjEW|Ec@`G_@jBG~GBjB@dCI~BYjG}@bE[fKs@~FSlD@fGNzAAnCIxAO~Bw@|@i@n@i@bBqB`A_Bt@yAhAyCt@}CpAuJhCwOj@aDtAaHrCmLLq@bG{WlCaLpAoD~AwCn@_A|@cArAmA~CsBvBw@f@OpDc@|BMh@ArBDnCR~B`@`KvBpO~DhBJFBnB@v@Cj@Qr@WbB{@dAo@rH}DjMgI`@_@t@}@~AqCp@eBz@uCr@gDxA{HfB{KbBwKlBsK|@sEnBqLz@gEhFoY\yBDa@f@{Cp@{B`AwBj@_Ah@k@h@g@jBeA|Cy@`GmA|Eq@hBInCBjBHv@FbANvBd@vHhClChAdEzAnA\bANt@FfB?jJgA~C{@`AQzAIfDIrBBrBFr@FlCJnHr@LLRDNTBXGRWVWLMB[GOKa@q@e@yCHy@N[VK^@ZTHNBZEh@MPQLa@HyEe@mE[gBGk@m@S]ASJE`A`@zANn@?PEjAo@V]^}@?YEaAMYMG]@qAHUDm@^[TSB",
                Fee = 115,
                StartingPointId = interestpoint1!.Id,
                EndingPointId = interestpoint2!.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Routes.Add(route);

            await context.SaveChangesAsync();
        }

        public static async Task SeedTrip(DataContext context)
        {
            if (await context.Trips.AnyAsync()) return;

            Trip trip = new()
            {
                StartedAt = DateTime.UtcNow,
                FinishedAt = DateTime.UtcNow,
                status = TripStatus.COMPLETED,
                PassengerId = 1,
                PaymentTransactionId = 1,
                PickUpPointId = 1,
                DropOffPointId = 1
            };

            context.Trips.Add(trip);

            await context.SaveChangesAsync();
        }

        public static async Task SeedFavoritePoint(DataContext context)
        {
            if (await context.FavoritePoints.AnyAsync()) return;
            var point = context.Points.Find(1);
            var route = context.Routes.Find(1);
            FavoritePoint favoritePoint = new()
            {
                PointId = point!.Id,
                PassengerId = 1,
                RouteId = route!.Id,
                CreatedAt = DateTime.UtcNow,
            };
            context.FavoritePoints.Add(favoritePoint);
            point.FavoritePoint!.Add(favoritePoint);


            await context.SaveChangesAsync();
        }

        public static async Task SeedOTP(DataContext context)
        {
            if (await context.OTPs.AnyAsync()) return;
            var passenger = await context.Passengers.FindAsync(1);
            var user = await context.Users.FindAsync(passenger!.UserId);
            if (passenger != null && user != null)
            {
                OTP otp = new()
                {
                    Otp = 1234,
                    PassengerEmail = user.Email,
                    CreatedAt = DateTime.UtcNow,
                };
                context.OTPs.Add(otp);
                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedCreditCards(DataContext context)
        {
            if (await context.CreditCards.AnyAsync()) return;
            CreditCard creditCard1 = new()
            {
                CardType = "VISA",
                CardNumber = 1234123412341234,
                CVC = 123,
                ExpirationDate = new DateOnly(2025, 1, 16),
                Balance = 999999999999999
            };
            CreditCard creditCard2 = new()
            {
                CardType = "MASTER_CARD",
                CardNumber = 4321432143214321,
                CVC = 321,
                ExpirationDate = new DateOnly(2025, 1, 16),
                Balance = 999999999999999
            };
            CreditCard creditCard3 = new()
            {
                CardType = "VISA",
                CardNumber = 1234567812345678,
                CVC = 123,
                ExpirationDate = new DateOnly(2025, 1, 16),
                Balance = 0
            };
            CreditCard creditCard4 = new()
            {
                CardType = "VISA",
                CardNumber = 1234561234561234,
                CVC = 123,
                ExpirationDate = new DateOnly(2023, 1, 16),
                Balance = 999999999999999
            };

            context.CreditCards.Add(creditCard1);
            context.CreditCards.Add(creditCard2);
            context.CreditCards.Add(creditCard3);
            context.CreditCards.Add(creditCard4);

            await context.SaveChangesAsync();
        }
    }
}