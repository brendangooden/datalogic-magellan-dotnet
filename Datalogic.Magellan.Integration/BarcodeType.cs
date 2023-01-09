using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Magellan.Integration
{
    [Flags]
    public enum BarcodeType
    {
        /// <summary>
        /// Codabar 1D barcode format. Codabar is a linear barcode symbology developed in 1972 by Pitney Bowes Corp. Note: Codabar encodes numerical data (digits) only.
        /// </summary>
        CodaBar = 1,
        /// <summary>
        /// Code 128 1D format. Code 128 is a high-density linear barcode symbology defined in ISO/IEC 15417:2007. It is used for alphanumeric or numeric-only barcodes.
        /// </summary>
        Code128 = 2,
        /// <summary>
        /// Code 39 1D barcode format. Code 39 is a variable length, discrete barcode symbology. The Code 39 specification defines 43 characters, consisting of uppercase letters (A through Z).
        /// </summary>
        Code39 = 4,
        /// <summary>
        /// Code 93 1D barcode format. Code 93 is a barcode symbology designed in 1982 by Intermec to provide a higher density and data security enhancement to Code 39. Code 93 Supports encoding of only the following ASCII characters: A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 0 1 2 3 4 5 6 7 8 9 - . $ / + % SPACE
        /// </summary>
        Code93 = 8,
        /// <summary>
        /// "RSS Expanded" or "GS1 Databar" barcode formats. Includes 1D barcode and stacked 2D variants of this barcode format.
        /// </summary>
        DataBar = 16,
        /// <summary>
        /// 
        /// </summary>
        DataBarExpanded = 32,
        /// <summary>
        /// Data Matrix 2D barcode format. An example of a Data Matrix code, encoding the text: "Wikipedia, the free encyclopedia" A Data Matrix is a two-dimensional barcode consisting of black and white "cells" or modules arranged in either a square or rectangular pattern, also known as a matrix. The information to be encoded can be text or numeric data. Usual data size is from a few bytes up to 1556 bytes.
        /// </summary>
        DataMatrix = 64,
        /// <summary>
        /// EAN-13 1D format. The International Article Number (also known as European Article Number or EAN) is a standard describing a barcode symbology and numbering system used in global trade to identify a specific retail product type, in a specific packaging configuration, from a specific manufacturer.Titf Please Note: EAN-13 may only encode numerical (digits) content of length 12 or 13 digits long. Shorter Barcodes will have trailing zeros (000) prepended to the start of the number automatically.
        /// </summary>
        EAN_13 = 128,
        /// <summary>
        /// EAN-8 1D barcode format. An EAN-8 is an EAN/UPC symbology barcode and is derived from the longer International Article Number (EAN-13) code. Please Note: EAN-8 may only encode numerical (digits) content of length 7 or 8 digits long. Shorter Barcodes will have trailing zeros (000) prepended to the start of the number automatically.
        /// </summary>
        EAN_8 = 256,
        /// <summary>
        /// ITF (Interleaved Two of Five) 1D format. ITF-14 is the GS1 implementation of an Interleaved 2 of 5 (ITF) bar code to encode a Global Trade Item Number. ITF-14 symbols are generally used on packaging levels of a product, such as a case box of 24 cans of soup. The ITF-14 will always encode 14 digits.s Please Note: ITF encodes numerical data only. If the number if digits is not even, a '0' will automatically be prepended.
        /// </summary>
        ITF = 512,
        /// <summary>
        /// MaxiCode 2D barcode format. MaxiCode is a public domain, machine-readable symbol system originally created and used by United Parcel Service. Suitable for tracking and managing the shipment of packages, it resembles a barcode, but uses dots arranged in a hexagonal grid instead .
        /// </summary>
        MaxiCode = 1024,
        /// <summary>
        /// QR Code 2D barcode format. QR code (abbreviated from Quick Response Code) is the trademark for a type of matrix barcode (or two-dimensional barcode) first designed in 1994 for the automotive industry in Japan. A barcode is a machine-readable optical label that contains information about the item to which it is attached. A QR code uses four standardized encoding modes (numeric, alphanumeric, byte/binary, and kanji) to efficiently store data; extensions may also be used.
        /// </summary>
        QR_Code = 2048,
        /// <summary>
        /// Reduce Space Symbology 14 barcode format. May represent a 1D barcode or Stacked 2D barcode. RSS 14 barcode (Reduce Space Symbology) encodes the full 14-digit EAN.UCC item identification in a symbol that can be omni-directionally scanned by suitably configured point-of-sale laser scanners. It is the latest barcode types for space-constrained identification from EAN International and the Uniform Code Council, Inc.. RSS barcodes have been identified to target the grocery industry and in healthcare, where items are too small to allow for other barcode symbologies.
        /// </summary>
        Rss14 = 4096,
        /// <summary>
        /// UPC-A 1D format. The Universal Product Code (UPC) is a barcode symbology that is widely used in the United States, Canada, United Kingdom, Australia, New Zealand, in Europe and other countries for tracking trade items in stores. UPC (technically refers to UPC-A) consists of 12 numeric digits, that are uniquely assigned to each trade item. Along with the related EAN barcode, the UPC is the barcode mainly used for scanning of trade items at the point of sale, per GS1 specifications. Please Note: UPCA may only encode numerical (digits) content of length 12 or 13 digits long. Shorter Barcodes will have trailing zeros (000) prepended to the start of the number automatically.
        /// </summary>
        UPC_A = 8192,
        /// <summary>
        /// UPC-E 1D format. To allow the use of UPC barcodes on smaller packages, where a full 12-digit barcode may not fit, a 'zero-suppressed' version of UPC was developed, called UPC-E, in which the number system digit, all trailing zeros in the manufacturer code, and all leading zeros in the product code, are suppressed (omitted).MSI Please Note: UPCE may only encode numerical (digits) content of length 7 or 8 digits long.
        /// </summary>
        UPC_E = 16384
    }
}
