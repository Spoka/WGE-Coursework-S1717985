using System.Collections;
using UnityEngine;
using System.Xml;

public class XMLVoxelFileWriter
{
    //write voxel chunk to XML file
    public static void SaveChunkToXMLFile(int [, ,] voxelArray, string fileName)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;

        //create write instance
        XmlWriter xmlWriter = XmlWriter.Create(fileName + ".xml", writerSettings);
        //write beginning of document
        xmlWriter.WriteStartDocument();
        //create root element
        xmlWriter.WriteStartElement("VoxelChunk");

        for (int x = 0; x < voxelArray.GetLength(0); x++)
        {
            for (int y = 0; y < voxelArray.GetLength(1); y++)
            {
                for (int z = 0; z < voxelArray.GetLength(1); z++)
                {
                    if (voxelArray[x, y, z] !=0)
                    {
                        //create single voxel element
                        xmlWriter.WriteStartElement("Voxel");
                        //store x index
                        xmlWriter.WriteAttributeString("x", x.ToString());
                        //store y index
                        xmlWriter.WriteAttributeString("y", y.ToString());
                        //store z index
                        xmlWriter.WriteAttributeString("z", z.ToString());
                        //store voxel type
                        xmlWriter.WriteString(voxelArray[x, y, z].ToString());
                        //end voxel element
                        xmlWriter.WriteEndElement();
                    }
                }
            }
        }

        //end root element
        xmlWriter.WriteEndElement();
        //write end of document
        xmlWriter.WriteEndDocument();
        //close document to save
        xmlWriter.Close();
    }

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
