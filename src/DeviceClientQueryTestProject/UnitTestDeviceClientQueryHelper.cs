using DeviceClientQueryLibrary;

namespace DeviceClientQueryTestProject
{
	[TestClass]
	public class UnitTestDeviceClientQueryHelper
	{
		[TestMethod]
		public void TestMethodQueryInUpperCaseWithOneArrayElementSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type IN ['audit']", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryInUpperCaseWithOneArrayElementFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type IN ['notaudit']", attributes);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TestMethodQueryInLowerCaseWithOneArrayElementSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type in ['audit']", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryInLowerCaseWithOneArrayElementFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type in ['notaudit']", attributes);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TestMethodQueryInUpperCaseWithMultipleArrayElementsSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type IN ['audit', 'a', 'AA',99]", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryInUpperCaseWithMultipleArrayElementsFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type IN ['notaudit', 'a', 'AA',99]", attributes);

			Assert.IsFalse(result);
		}


		[TestMethod]
		public void TestMethodQueryInLowerCaseWithMultipleArrayElementsSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type in ['audit', 'a', 'AA',99]", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryInLowerCaseWithMultipleArrayElementsFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type in ['notaudit', 'a', 'AA',99]", attributes);

			Assert.IsFalse(result);
		}


		[TestMethod]
		public void TestMethodQueryWithEqualsSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type = ""audit""", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryWithEqualsFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type = ""notaudit""", attributes);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TestMethodQueryWithNotEqualsSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type != ""notaudit""", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryWithNotEqualsFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type != ""audit""", attributes);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TestMethodQueryWithNotEquals2Success()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type <> ""notaudit""", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryWithNotEquals2Fail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type <> ""audit""", attributes);

			Assert.IsFalse(result);
		}
	}
}