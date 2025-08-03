using System;
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
            public byte[] normal_data     = new byte[12];
            public byte[] vertex1_data    = new byte[12];
            public byte[] vertex2_data    = new byte[12];
            public byte[] vertex3_data    = new byte[12];
            public byte[] attr_byte_count = new byte[2];
        }
        static STLReader()
        {

        }

        public static string ReadSTLBinary( string filename )
        {
            UINT32 num_triangles = 0;
            int ret_check = 0;
            byte[] header = new byte[80];
            Triangle[] triangles;
            FileStream reader = new FileStream(filename, FileMode.Open);
            
            ret_check = reader.Read(header, 0, 80);
            if (ret_check != 80)
            {
                // Problem in the data
            }

            ret_check = reader.Read(num_triangles, 80, 4);
            if (ret_check != 4)
            {
                // Problem in the data
            }

            triangles = new Triangle[num_triangles];

            foreach ( Triangle tri in triangles)
            {
                ret_check = reader.Read(tri.normal_data, 0, 12);
                if (ret_check != 12)
                {
                    // Problem in the data
                }

                ret_check = reader.Read(tri.vertex1_data, 0, 12);
                if (ret_check != 12)
                {
                    // Problem in the data
                }

                ret_check = reader.Read(tri.vertex2_data, 0, 12);
                if (ret_check != 12)
                {
                    // Problem in the data
                }

                ret_check = reader.Read(tri.vertex3_data, 0, 12);
                if (ret_check != 12)
                {
                    // Problem in the data
                }

                ret_check = reader.Read(tri.attr_byte_count, 0, 2);
                if (ret_check != 2)
                {
                    // Problem in the data
                }
            }
            return "";
        }
    }
}
