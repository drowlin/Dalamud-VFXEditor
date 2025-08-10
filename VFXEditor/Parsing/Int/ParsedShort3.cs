using Dalamud.Bindings.ImGui;
using System.IO;
using Int4 = SharpDX.Int4;
using System.Numerics;

namespace VfxEditor.Parsing.Int {
    public class ParsedShort3 : ParsedInt4 {
        public ParsedShort3( string name, Int4 value ) : base( name, value ) { }

        public ParsedShort3( string name ) : base( name ) { }

        public override void Read( BinaryReader reader ) => Read( reader, 0 );

        public override void Read( BinaryReader reader, int _ ) {
            Value.X = reader.ReadInt16();
            Value.Y = reader.ReadInt16();
            Value.Z = reader.ReadInt16();
        }

        public override void Write( BinaryWriter writer ) {
            writer.Write( ( short )Value.X );
            writer.Write( ( short )Value.Y );
            writer.Write( ( short )Value.Z );
        }

        protected override void DrawBody() {
            var value = Value.ToArray();
            var value_vec = new Vector3( value[0], value[1], value[2] );
            if( ImGui.InputFloat3( Name, ref value_vec ) ) {
                value[0] = ( short )value_vec.X;
                value[1] = ( short )value_vec.Y;
                value[2] = ( short )value_vec.Z;
                Update( value );
            }
        }
    }
}
