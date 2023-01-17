using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;



public class NFTCheckout : MonoBehaviour
{
    [SerializeField] private string account = "0x7a53325D1C36Eea7BbE8C6a8D00f2a0efd580e77";
    [SerializeField] private string contract = "0x2953399124F0cBB46d2CbACD8A89cF0599974963";
    [SerializeField] private string tokenId = "55329163431098826836481317006890444882277269896862461227869301097970473631745";
    [Space] 
    [SerializeField] private MeshRenderer meshRenderer;
    
    [HideInInspector] public string chain = "polygon";
    [HideInInspector] public string network = "mainnet";

    
    private Metadata metadata;

    private async void Start()
    {
        await BalanceOf();
        
        string metaDataUrl = await GetMetaDataUrl(chain, network, contract);

        LoadImageFromNftMetadata(metaDataUrl);
    }

    private async void LoadImageFromNftMetadata(string metadataUrl)
    {
        // Load metadata
        using (UnityWebRequest webRequest = UnityWebRequest.Get(metadataUrl))
        {
            await webRequest.SendWebRequest();
    
            var metadataString = webRequest.downloadHandler.text;
    
            metadata = JsonConvert.DeserializeObject<Metadata>(metadataString);
        }
        
        // Load texture
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(metadata.image))
        {
            await webRequest.SendWebRequest();
    
            var texture = DownloadHandlerTexture.GetContent(webRequest);
    
            meshRenderer.material.mainTexture = texture;
        }
    }

    private async Task BalanceOf()
    {
        BigInteger amount = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        
        print("Amount of NFTs: " + amount);
    }

    private async Task<string> GetMetaDataUrl(string chain, string network, string contract)
    {
        var uri = await ERC1155.URI(chain, network, contract, tokenId);
        
        uri = uri.Replace("0x{id}", tokenId);
        
        print(uri);

        return uri;
    }
    
    public class Metadata
    {
        public List<Attributes> attributes { get; set; }
        public string description { get; set; }
        public string external_url { get; set; }
        public string image { get; set; }
        public string name { get; set; }
    }
    
    public class Attributes
    {
        public string trait_type;
        public string value;
    }
}


