# SecureFileEncryption
A C# application that encrypts and decrypts files using AES encryption.

# Secure File Encryption Tool

A C# application that encrypts and decrypts files using AES encryption to ensure data confidentiality.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Requirements](#requirements)
- [Usage](#usage)
- [Encryption Details](#encryption-details)
- [Example](#example)
- [Building the Project](#building-the-project)
- [Contributing](#contributing)
- [License](#license)

## Overview

Data security is crucial in today's digital age. This tool allows users to securely encrypt and decrypt files, protecting sensitive information from unauthorized access.

## Features

- **File Encryption**: Encrypt any file using a password.
- **File Decryption**: Decrypt files using the correct password.
- **Strong Encryption Algorithm**: Uses AES with a 256-bit key.
- **Salt and Key Derivation**: Securely generates keys using password-based key derivation and random salt.
- **Large File Handling**: Efficiently processes large files with a buffered read/write.

## Requirements

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

## Usage

1. **Clone the repository**:

   ```bash
   git clone https://github.com/yourusername/SecureFileEncryption.git


Encrypting a file:

=== Secure File Encryption Tool ===

Choose an option:
1. Encrypt a file
2. Decrypt a file

Option: 1

Enter the full path of the file to encrypt: C:\Users\Username\Documents\secret.txt
Enter a password: ********
Enter the output file path: C:\Users\Username\Documents\secret.enc

File encrypted successfully!


decrypting a file:

=== Secure File Encryption Tool ===

Choose an option:
1. Encrypt a file
2. Decrypt a file

Option: 2

Enter the full path of the file to decrypt: C:\Users\Username\Documents\secret.enc
Enter the password: ********
Enter the output file path: C:\Users\Username\Documents\secret_decrypted.txt

File decrypted successfully!
