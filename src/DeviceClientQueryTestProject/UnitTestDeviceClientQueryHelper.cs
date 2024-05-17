using DeviceClientQueryLibrary;

namespace DeviceClientQueryTestProject
{
	[TestClass]
	public class UnitTestDeviceClientQueryHelper
	{
		[TestMethod]
		public void TestMethodQueryInWithOneArrayElementSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type IN ['audit']", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryInWithOneArrayElementFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type IN ['notaudit']", attributes);

			Assert.IsFalse(result);
		}


		[TestMethod]
		public void TestMethodQueryInWithMultipleArrayElementsSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type IN ['audit', 'a', 'AA',99]", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryInWithMultipleArrayElementsFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query("attributes.type IN ['notaudit', 'a', 'AA',99]", attributes);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TestMethodQueryInWithEqualsSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type = ""audit""", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryInWithEqualsFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type = ""notaudit""", attributes);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TestMethodQueryInWithNotEqualsSuccess()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type != ""notaudit""", attributes);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void TestMethodQueryInWithNotEqualsFail()
		{
			KeyValuePair<string, System.BinaryData> attributes = new KeyValuePair<string, BinaryData>("type", new BinaryData("audit"));

			var result = DeviceClientQueryHelper.Query(@"attributes.type != ""audit""", attributes);

			Assert.IsFalse(result);
		}
	}
}