param
(
[Parameter(Mandatory = $true)]
[string]	      
$FilePath
)
$password = ConvertTo-SecureString -String "1234" -Force -AsPlainText
$cert = Get-PfxCertificate -FilePath ExampleCodeSigning.pfx
$signature = Set-AuthenticodeSignature -FilePath $FilePath -Certificate $cert -TimestampServer http://timestamp.comodoca.com/authenticode
Write-Host $signature.StatusMessage
