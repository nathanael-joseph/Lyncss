/*
    A very basic block data structure. 
    For now, the Content is a string, which allows us to store any serialised data in the blockchain.
*/
using System;

namespace LyncssConsole
{
    class Block
    {
        public int Nonce {get; private set;}
        public int Id {get; set;}
        public string Publisher {get; set;} // Account / Public Key of the publisher (this will be the minor)
        public string PreviousHash {get; set;} // hashcode of previous block
        public string Content {get; set;} 
        public string Signature {get; set;} // minor creates signature for Publisher + PeviousHash + Content 
        public string Hash {get; set;}

        public string GetSerializedBlockContents => $"{Nonce}#{Publisher}#{PreviousHash}#{Content}#{Signature}";

        public bool HasValidHash =>
             hashMeetsFormatRequirements && hashMatchesSerializedContent;

        public string ExpectedHash =>
            Services.GetSha256HashHexString(GetSerializedBlockContents);
        private bool hashMatchesSerializedContent =>
            Services.GetSha256HashHexString(GetSerializedBlockContents)
                    .Equals(Hash, StringComparison.OrdinalIgnoreCase);

        private bool hashMeetsFormatRequirements  => 
            Services.BlockHashRegex.IsMatch(Hash);

        public void Mine(int seed) 
        {
            Nonce = seed;
            string tryHash = ExpectedHash;
            while(! Services.BlockHashRegex.IsMatch(tryHash)) {
                Nonce++;
                tryHash = ExpectedHash;
            }

            Hash = tryHash;
        }

        public void Mine() => Mine(0);
    
        public void Print()
        {
            Console.WriteLine($"Nonce [{Nonce}]");
            Console.WriteLine($"Id [{Id}]");
            Console.WriteLine($"Publisher [{Publisher}]");
            Console.WriteLine($"PreviousHash [{PreviousHash}]");
            Console.WriteLine($"Content [{Content}]");
            Console.WriteLine($"Signature [{Signature}]");
            Console.WriteLine($"Hash [{Hash}]");
        } 
    }

}