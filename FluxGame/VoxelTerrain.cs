using System;
using System.Collections.Generic;
using Flux;
using Flux.Core;
using SimplexNoise;

namespace FluxGame
{
    public static class VoxelTerrain
    {
        const int WIDTH = 320;
        const int HEIGHT = 320;
        #region cubedata
        // Cube vertex positions
        private static readonly float[] cubeVerts = {
        // Front face
        0, 0, 1,
        1, 0, 1,
        1, 1, 1,
        0, 1, 1,

        // Back face
        0, 0, 0,
        0, 1, 0,
        1, 1, 0,
        1, 0, 0,

        // Left face
        0, 0, 0,
        0, 0, 1,
        0, 1, 1,
        0, 1, 0,

        // Right face
        1, 0, 0,
        1, 1, 0,
        1, 1, 1,
        1, 0, 1,

        // Top face
        0, 1, 0,
        0, 1, 1,
        1, 1, 1,
        1, 1, 0,

        // Bottom face
        0, 0, 0,
        1, 0, 0,
        1, 0, 1,
        0, 0, 1
    };

        // Cube normals
        private static readonly float[] cubeNormals = {
        // Front face
        0, 0, 1,
        0, 0, 1,
        0, 0, 1,
        0, 0, 1,

        // Back face
        0, 0, -1,
        0, 0, -1,
        0, 0, -1,
        0, 0, -1,

        // Left face
        -1, 0, 0,
        -1, 0, 0,
        -1, 0, 0,
        -1, 0, 0,

        // Right face
        1, 0, 0,
        1, 0, 0,
        1, 0, 0,
        1, 0, 0,

        // Top face
        0, 1, 0,
        0, 1, 0,
        0, 1, 0,
        0, 1, 0,

        // Bottom face
        0, -1, 0,
        0, -1, 0,
        0, -1, 0,
        0, -1, 0
    };

        // Cube UVs (all zeros for now)
        private static readonly float[] cubeUVs = {
    // Front face
    0, 0,   // Bottom-left
    1, 0,   // Bottom-right
    1, 1,   // Top-right
    0, 1,   // Top-left
    
    // Back face
    0, 0,   // Bottom-left
    1, 0,   // Bottom-right
    1, 1,   // Top-right
    0, 1,   // Top-left
    
    // Left face
    0, 0,   // Bottom-left
    1, 0,   // Bottom-right
    1, 1,   // Top-right
    0, 1,   // Top-left
    
    // Right face
    0, 0,   // Bottom-left
    1, 0,   // Bottom-right
    1, 1,   // Top-right
    0, 1,   // Top-left
    
    // Top face
    0, 0,   // Bottom-left
    1, 0,   // Bottom-right
    1, 1,   // Top-right
    0, 1,   // Top-left
    
    // Bottom face
    0, 0,   // Bottom-left
    1, 0,   // Bottom-right
    1, 1,   // Top-right
    0, 1    // Top-left
};

        // Cube indices (for 2 triangles per face)
        private static readonly uint[] cubeIndices = {
        0, 1, 2,  2, 3, 0,      // Front face
        4, 5, 6,  6, 7, 4,      // Back face
        8, 9, 10, 10, 11, 8,    // Left face
        12, 13, 14, 14, 15, 12, // Right face
        16, 17, 18, 18, 19, 16, // Top face
        20, 21, 22, 22, 23, 20  // Bottom face
    };
        #endregion
        public static void GenerateTerrain(out float[] verts, out float[] normals, out float[] uvs, out uint[] indices,int xOffset, int zOffset)
        {
            List<float> vertList = new List<float>();
            List<float> normalList = new List<float>();
            List<float> uvList = new List<float>();
            List<uint> indexList = new List<uint>();
            uint vertexOffset = 0;

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    int xPos = x + xOffset*WIDTH;
                    int yPos = y + zOffset*HEIGHT;
                    float scale = 3f; //5 is good too
                    float hillNoise = Noise.CalcPixel2D(xPos, yPos, 0.003f * scale) * .15f;
                    float mountainNoise = Noise.CalcPixel2D(xPos, yPos, 0.0005f * scale) * .2f;
                    float erosionNoise = Noise.CalcPixel2D(xPos, yPos, 0.0003f * scale) * .015f;
                    float detailnoise = Noise.CalcPixel2D(xPos, yPos, 0.009f * scale * 0.4f) * 0.05f;
                    float detailnoise2 = Noise.CalcPixel2D(xPos, yPos, 0.035f * scale * 0.4f) * 0.05f;
                    detailnoise = MathExt.Lerp(detailnoise, detailnoise2, 0.2f);
                    detailnoise = MathExt.Lerp(detailnoise, 1, 0.9f);
                    float finalNoise = MathExt.Lerp(hillNoise, mountainNoise, erosionNoise) * (detailnoise * erosionNoise * 0.2f);

                    int height = (int)MathF.Round((finalNoise * .75f));

                    AddCube(x+ xOffset * WIDTH, height, y+zOffset * HEIGHT, ref vertList, ref normalList, ref uvList, ref indexList, ref vertexOffset);
                    if (finalNoise * 0.5f > 34)
                    {
                        //AddCube(x + xOffset, -1 + height, y + zOffset, ref vertList, ref normalList, ref uvList, ref indexList, ref vertexOffset);
                        //AddCube(x + xOffset, -2 + height, y + zOffset, ref vertList, ref normalList, ref uvList, ref indexList, ref vertexOffset);
                        //if(finalNoise * 0.5f > 56)
                        //AddCube(x + xOffset, -2 + height, y + zOffset, ref vertList, ref normalList, ref uvList, ref indexList, ref vertexOffset);
                    }
                }
            }

            verts = vertList.ToArray();
            normals = normalList.ToArray();
            uvs = uvList.ToArray();
            indices = indexList.ToArray();
        }

        private static void AddCube(int x, int y, int z, ref List<float> verts, ref List<float> normals, ref List<float> uvs, ref List<uint> indices, ref uint vertexOffset)
        {
            for (int i = 0; i < cubeVerts.Length; i += 3)
            {
                verts.Add(cubeVerts[i] + x);
                verts.Add(cubeVerts[i + 1] + y);
                verts.Add(cubeVerts[i + 2] + z);
            }
            normals.AddRange(cubeNormals);
            uvs.AddRange(cubeUVs);

            for (int i = 0; i < cubeIndices.Length; i++)
            {
                indices.Add(cubeIndices[i] + vertexOffset);
            }
            vertexOffset += 24;
        }
    }

}
