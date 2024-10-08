#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Tls.Crypto.Impl.BC
{
    /// <summary>BC light-weight base class for the signers implementing the two DSA style algorithms from FIPS PUB
    /// 186-4: DSA and ECDSA.</summary>
    public abstract class BcTlsDssSigner
        : BcTlsSigner
    {
        protected BcTlsDssSigner(BcTlsCrypto crypto, AsymmetricKeyParameter privateKey)
            : base(crypto, privateKey)
        {
        }

        protected abstract IDsa CreateDsaImpl(int cryptoHashAlgorithm);

        protected abstract short SignatureAlgorithm { get; }

        public override byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, byte[] hash)
        {
            if (algorithm != null && algorithm.Signature != SignatureAlgorithm)
                throw new InvalidOperationException("Invalid algorithm: " + algorithm);

            int cryptoHashAlgorithm = (null == algorithm)
                ? CryptoHashAlgorithm.sha1
                : TlsCryptoUtilities.GetHash(algorithm.Hash);

            ISigner signer = new DsaDigestSigner(CreateDsaImpl(cryptoHashAlgorithm), new NullDigest());
            signer.Init(true, new ParametersWithRandom(m_privateKey, m_crypto.SecureRandom));
            if (algorithm == null)
            {
                // Note: Only use the SHA1 part of the (MD5/SHA1) hash
                signer.BlockUpdate(hash, 16, 20);
            }
            else
            {
                signer.BlockUpdate(hash, 0, hash.Length);
            }
            try
            {
                return signer.GenerateSignature();
            }
            catch (CryptoException e)
            {
                throw new TlsFatalAlert(AlertDescription.internal_error, e);
            }
        }
    }
}
#pragma warning restore
#endif
