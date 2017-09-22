﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestUpdateCardValidationNumOnToken
    {
        
        private CnpOnline cnp;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestSimpleRequest()
        {
            updateCardValidationNumOnToken update = new updateCardValidationNumOnToken();
            update.orderId = "12344";
            update.cnpToken = "1111222233334444";
            update.cardValidationNum = "321";
            update.id = "123";
            update.reportGroup = "Default Report Group";
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<updateCardValidationNumOnToken id=\"123\" reportGroup=\"Default Report Group\".*<orderId>12344</orderId>.*<cnpToken>1111222233334444</cnpToken>.*<cardValidationNum>321</cardValidationNum>.*</updateCardValidationNumOnToken>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><updateCardValidationNumOnTokenResponse><cnpTxnId>4</cnpTxnId><orderId>12344</orderId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></updateCardValidationNumOnTokenResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.setCommunication(mockedCommunication);
            cnp.UpdateCardValidationNumOnToken(update);
        }

        [Test]
        public void TestOrderIdIsOptional()
        {
            updateCardValidationNumOnToken update = new updateCardValidationNumOnToken();
            update.orderId = null;
            update.cnpToken = "1111222233334444";
            update.cardValidationNum = "321";
            update.id = "123";
            update.reportGroup = "Default Report Group";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<updateCardValidationNumOnToken id=\"123\" reportGroup=\"Default Report Group\".*<cnpToken>1111222233334444</cnpToken>.*<cardValidationNum>321</cardValidationNum>.*</updateCardValidationNumOnToken>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
            //mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><updateCardValidationNumOnTokenResponse><cnpTxnId>4</cnpTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></updateCardValidationNumOnTokenResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.setCommunication(mockedCommunication);
            updateCardValidationNumOnTokenResponse response = cnp.UpdateCardValidationNumOnToken(update);
            Assert.IsNotNull(response);

        }

    }
}
