using System.Collections;
using UnityEngine;
using System.Xml;

public class XMLVoxelFileReader
{
    //read voxel chunk from XML file
    public static int[, ,] LoadChunkFromXMLFile(int size, string fileName)
    {
        int[,,] voxelArray = new int[size, size, size];
        //create xml reader
        XmlReader xmlReader = XmlReader.Create(fileName + ".xml");
        //iterate and read every line in xml file
        while (xmlReader.Read())
        {
            //check if node has a Voxel element
            if (xmlReader.IsStartElement("Voxel"))
            {
                //parse x from string to int
                int x = int.Parse(xmlReader["x"]);
                //same for y
                int y = int.Parse(xmlReader["y"]);
                //and z
                int z = int.Parse(xmlReader["z"]);
                xmlReader.Read();
                int value = int.Parse(xmlReader.Value);
                voxelArray[x, y, z] = value;
            }
        }   
        return voxelArray;
    }
}
