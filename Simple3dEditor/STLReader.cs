using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Simple3dEditor
{
    static class STLReader
    {
        // An STL Binary consists of the following:
        // UINT8[80]    – Header                 - 80 bytes
        // UINT32       – Number of triangles    - 04 bytes
        // foreach triangle                      - 50 bytes
        //     REAL32[3] – Normal vector         - 12 bytes
        //     REAL32[3] – Vertex 1              - 12 bytes
        //     REAL32[3] – Vertex 2              - 12 bytes
        //     REAL32[3] – Vertex 3              - 12 bytes
        //     UINT16    – Attribute byte count  - 02 bytes
        // end
        //
        // Take that binary data in, and translate it to a string fitting a System.Windows.Media.Media3D.MeshGeometry3D
        // The MeshGeometry3D will be displayed on the ViewPort3D
        
        private class Triangle
        {
            public byte[] normal_data;
            public byte[] vertex1_data;
            public byte[] vertex2_data;
            public byte[] vertex3_data;
            public byte[] attr_byte_count;

            public Triangle()
            {
                this.normal_data = new byte[12];
                this.vertex1_data = new byte[12];
                this.vertex2_data = new byte[12];
                this.vertex3_data = new byte[12];
                this.attr_byte_count = new byte[2];
        }
        }
        static STLReader()
        {

        }

        public static (Vector3DCollection, Point3DCollection) ReadSTLBinary( string filename )
        {
            int ret_check         = 0;
            int num_triangles     = 0;
            byte[] read_triangles = new byte[4];
            byte[] header         = new byte[80];
            
            FileStream reader                = new FileStream(filename, FileMode.Open, FileAccess.Read);
            Vector3DCollection stl_vectors   = new Vector3DCollection();
            Point3DCollection  stl_verticies = new Point3DCollection();
            Triangle[] triangles;

            ret_check = reader.Read(header, 0, 80);
            if (ret_check != 80)
            {
                // Problem in the data
            }

            ret_check = reader.Read(read_triangles, 0, 4);
            if (ret_check != 4)
            {
                // Problem in the data
            }

            num_triangles = BitConverter.ToInt32(read_triangles, 0);
            triangles = new Triangle[num_triangles];

            for (int i = 0; i < num_triangles; i++)
            {
                triangles[i] = new Triangle();

                ret_check = reader.Read(triangles[i].normal_data, 0, 12);
                if (ret_check != 12)
                {
                    // Problem in the data
                }
                stl_vectors.Add(new Vector3D(BitConverter.ToSingle(triangles[i].normal_data, 0), BitConverter.ToSingle(triangles[i].normal_data, 4), BitConverter.ToSingle(triangles[i].normal_data, 8)));

                ret_check = reader.Read(triangles[i].vertex1_data, 0, 12);
                if (ret_check != 12)
                {
                    // Problem in the data
                }
                stl_verticies.Add(new Point3D(BitConverter.ToSingle(triangles[i].vertex1_data, 0), BitConverter.ToSingle(triangles[i].vertex1_data, 4), BitConverter.ToSingle(triangles[i].vertex1_data, 8)));
                
                ret_check = reader.Read(triangles[i].vertex2_data, 0, 12);
                if (ret_check != 12)
                {
                    // Problem in the data
                }
                stl_verticies.Add(new Point3D(BitConverter.ToSingle(triangles[i].vertex2_data, 0), BitConverter.ToSingle(triangles[i].vertex2_data, 4), BitConverter.ToSingle(triangles[i].vertex2_data, 8)));

                ret_check = reader.Read(triangles[i].vertex3_data, 0, 12);
                if (ret_check != 12)
                {
                    // Problem in the data
                }
                stl_verticies.Add(new Point3D(BitConverter.ToSingle(triangles[i].vertex3_data, 0), BitConverter.ToSingle(triangles[i].vertex3_data, 4), BitConverter.ToSingle(triangles[i].vertex3_data, 8)));

                ret_check = reader.Read(triangles[i].attr_byte_count, 0, 2);
                if (ret_check != 2)
                {
                    // Problem in the data
                }
            }
            return (stl_vectors, stl_verticies);
        }
    }
}
