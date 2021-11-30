/*
    A very basic block data structure. 
    For now, the Content is a string, which allows us to store any serialised data in the blockchain.
*/

using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LyncssConsole
{
    class Block
    {
        public int Nonce {get; private set;}
        public string Publisher {get; set;} // Account / Public Key of the publisher (this will be the minor)
        public string PreviousHash {get; set;} // hashcode of previous block
        public string Content {get; set;} 
        public string Signature {get; set;} // minor creates signature for Publisher + PeviousHash + Content 
        public string Hash {get; set;}

        public string GetSerializedBlockContents() {
            return  $"{Nonce}#{Publisher}#{PreviousHash}#{Content}#{Signature}";
        }
        public bool HasValidHash() { 
            // todo
            return false;
        }

        private bool hashMatchesSerializedContent() {
            return SHA256.HashData(
                        Encoding.UTF8.GetBytes(GetSerializedBlockContents())
                    ) == Encoding.UTF8.GetBytes(Hash);
        }

        // private bool hashMeetsFormatRequirements() {
        //     Regex rgx = new Regex("^0{3}");
        // }
    }
}