# Crypto
Библиотека с оболочкой над различными криптопротоколами ( AES, RSA )

## Использование AES
```C#
AES aes = new AES();
string helloword = "Hello word!";
string ciphertext = aes.Encrypt(helloword);   // ciphertext = "6a9IaUHeSzdExjF7HSIJdw=="
string plaintext = aes.Decrypt(ciphertext);   // plaintext = "Hello word!"
```
### Ты так же можешь задать размер для ключа или указать сам ключ
```C#
AES aes = new AES(AES.KeySizeBit.Small);
aes = new AES(yourKey, yourIV);
```

### Полезно знать
```C#
AES aes = new AES();

aes.Key_IV;   // Возвращает ключ и iv разделенные "\" - обратным слешом

aes.SetKey(yourKey, yourIV);   // устанавливает новый ключ

aes.GenerateKey_IV(AES.KeySizeBit.Small);   // генерирует новый ключ заданной длинны
```
## Использование RSA
```C#
RSA rsa = new RSA();
string helloword = "Hello word!";
string ciphertext = rsa.Encrypt(helloword);   // ciphertext = "Yn1JtqBb+zRynzBixlk7IZbR6voC9hMKadQvzAatYCT7dt/L3VVRNGj1Y7vK2BNYFul2syIYjd+s/w4ZAZAKVPNO9yvC7/SC0GzBjWTgz6x1gsa6z19a7tBTRb7fyNF/pDXNypsvdYgBjMMTV3H4PkBtgLLhT5+OFro1KHwnEJk="
string plaintext = rsa.Decrypt(ciphertext);   // plaintext = "Hello word!"
```
### Ты так же можешь задать размер для ключа или указать сам ключ
```C#
RSA rsa = new RSA(RSA.KeySizeBit.Small);
rsa = new RSA(KeyType, Key); // KeyType.Public or KeyType.Private
rsa = new RSA(publicKey, privateKey);
```

### Полезно знать
```C#
RSA rsa = new RSA();

rsa.PublicKey;   // Возвращает открытый ключ

rsa.SetKey(KeyType.Public, Key);   // устанавливает новый публичный ключ
rsa.SetKey(KeyType.Private, Key);   // устанавливает новый приватный ключ
rsa.SetKeys(publicKey, privateKey);   // устанавливает новый публичный и приватный ключ

rsa.GenerateKeys(RSA.KeySizeBit.Small);   // генерирует новый публичный и приватный ключ заданной длинны
```

### Ты можешь использовать интерфейс IEncryptor для передачи AES или RSA
```C#
IEncryptor encryptor = new AES();

//your code

encryptor = new RSA();
```
### _Все методы перегружены и могут работать как со строками так и с массивом байтов_

