using Dalamud.Bindings.ImGui;
using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace VfxEditor.Parsing.Int {
    public class ParsedIntByte4 : ParsedSimpleBase<int> {
        public ParsedIntByte4( string name, int value ) : base( name, value ) { }

        public ParsedIntByte4( string name ) : base( name ) { }

        public override void Read( BinaryReader reader ) => Read( reader, 0 );

        public override void Read( BinaryReader reader, int size ) {
            Value = reader.ReadInt32();
        }

        public override void Write( BinaryWriter writer ) => writer.Write( Value );

        protected override void DrawBody() {
            var bytes = BitConverter.GetBytes( Value );
            var value = bytes.Select( x => ( int )x ).ToArray();
            var value_vec = new Vector4( value[0], value[1], value[2], value[3] );
            if( ImGui.InputFloat4( Name, ref value_vec ) )
            {
                value[0] = ( int )value_vec.X;
                value[1] = ( int )value_vec.Y;
                value[2] = ( int )value_vec.Z;
                value[3] = ( int )value_vec.W;
                var newValue = BitConverter.ToInt32( value.Select( x => ( byte )x ).ToArray() );
                Update( newValue );
            }
        }
    }
}
