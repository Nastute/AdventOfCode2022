var datastreams = FileReader.FileReader.FileToArray(@"..\..\..\day6RealInput.txt").ToList();
ParseDataStream(datastreams);

public static partial class Program
{
    public static void ParseDataStream(List<string> datastreams)
    {
        var packetList = new List<int>();
        var messageList = new List<int>();
        foreach (var datastream in datastreams)
        {
            var packetMarker = FindPacketMarker(datastream);
            packetList.Add(packetMarker);

            var messageMarker = FindMessageMarker(datastream);
            messageList.Add(messageMarker);
        }

        Console.WriteLine("packet");
        packetList.ForEach(packet => Console.Write(packet + " "));
        Console.WriteLine();
        Console.WriteLine("message");
        messageList.ForEach(message => Console.Write(message + " "));
    }

    public static int FindPacketMarker(string datastream)
    {
        for (int i = 0; i < datastream.Length; i++)
        {
            var temp = datastream.Substring(i, 4).Distinct();
            if (temp.Count() < 4)
            {
                continue;
            }
            var number = datastream.IndexOf(string.Join("", temp));
            return number + 4;
        }
        return -1;
    }

    public static int FindMessageMarker(string datastream)
    {
        for (int i = 0; i < datastream.Length; i++)
        {
            var temp = datastream.Substring(i, 14).Distinct();
            if (temp.Count() < 14)
            {
                continue;
            }
            var number = datastream.IndexOf(string.Join("", temp));
            return number + 14;
        }
        return -1;
    }
}

