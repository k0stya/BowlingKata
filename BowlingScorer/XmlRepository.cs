using System.IO;
using System.Xml.Serialization;

namespace BowlingScorer
{
	public class XmlRepository : IRepository
	{
		public void Save(int?[] statistics)
		{
			string path = Path.Combine(Path.GetTempPath(), "statistics.xml");
			using (var writer = File.OpenWrite(path))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(int?[]));
				serializer.Serialize(writer, statistics);
				writer.Close();
			}
		}

		public int?[] Load()
		{
			int?[] statistics = new int?[21];
			string path = Path.Combine(Path.GetTempPath(), "statistics.xml");
			using (var reader = File.OpenText(path))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(int?[]));
				statistics = (int?[])serializer.Deserialize(reader);
				reader.Close();
			}
			return statistics;
		}
	}
}