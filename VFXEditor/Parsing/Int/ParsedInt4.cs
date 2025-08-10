using Dalamud.Bindings.ImGui;
using System;
using System.IO;
using System.Numerics;
using Int4 = SharpDX.Int4;

namespace VfxEditor.Parsing {
    public class ParsedInt4 : ParsedSimpleBase<Int4> {
        public ParsedInt4( string name, Int4 value ) : base( name, value ) { }

        public ParsedInt4( string name ) : base( name ) { }

        public override void Read( BinaryReader reader ) => Read( reader, 0 );

        public override void Read( BinaryReader reader, int _ ) {
            Value.X = reader.ReadInt32();
            Value.Y = reader.ReadInt32();
            Value.Z = reader.ReadInt32();
            Value.W = reader.ReadInt32();
        }

        public override void Write( BinaryWriter writer ) {
            writer.Write( Value.X );
            writer.Write( Value.Y );
            writer.Write( Value.Z );
            writer.Write( Value.W );
        }

        protected override void DrawBody() {
            var value = Value.ToArray();
            var value_vec = new Vector4( value[0], value[1], value[2], value[3] );
            if( ImGui.InputFloat4( Name, ref value_vec ) )
            {
                value[0] = ( int )value_vec.X;
                value[1] = ( int )value_vec.Y;
                value[2] = ( int )value_vec.Z;
                value[3] = ( int )value_vec.W;
                Update( value );
            }
        }
    }
}