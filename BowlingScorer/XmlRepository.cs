using System.IO;
using System.Xml.Serialization;

namespace BowlingScorer
{
	public class XmlRepository : IRepository
	{
		public void Save(Frame[] statistics)
		{
			string path = Path.Combine(Path.GetTempPath(), "statistics.xml");
			using (var writer = File.OpenWrite(path))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Frame[]));
				serializer.Serialize(writer, statistics);
				writer.Close();
			}
		}

		public Frame[] Load()
		{
			Frame[] statistics = new Frame[10];
			string path = Path.Combine(Path.GetTempPath(), "statistics.xml");
			using (var reader = File.OpenText(path))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Frame[]));
				statistics = (Frame[])serializer.Deserialize(reader);
				reader.Close();
			}
			return statistics;
		}
	}
}