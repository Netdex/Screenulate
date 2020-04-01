# Screenulate
`This project is a work-in-progress.`

Optical character recognition of Japanese phrases. Kanji and reading lookup with JMdict/KANJIDIC. 
Rikaichan-like tokenization and parsing. All so that I can read my visual novels.
![screenshot](https://i.imgur.com/lko3pAi.png)

Uses Tesseract-OCR for OCR, then tokenizes text for dictionary lookup. 
The goal of this project is not machine translation, it is for quick lookup of unfamiliar words/phrases/places. 

## Usage
```
ALT+Q           Enter screen capture mode
` (OEM_TILDE)   Reuse previous screen capture region
```

In screen capture mode, click and drag to select a region to read (will appear as a red rectangle). The previous region you selected will appear as a green rectangle. In screen capture mode,

```
ESC             Exit screen capture mode
SPACE           Reuse previous screen capture region
```

After you complete the capture, the source text and translated text will appear in Screenulate.

## FAQ

### Is it done?
No.

### Why does it take so long before tokenization is ready?
Creating the structures required for parsing takes some time (and about a gigabyte of RAM).

### Why is text not being recognized properly?
Try these following things in order:

- Examine if the post-processed image is unclear/unreadable.
	- If the image is too small, try enlargening the source text if possible (even if it's a resampled scale, it will help).
	- If the image is noisy, or there are bridges between characters,
		- Remove any non-solid background behind the text if possible
		- Use the context menu of the scan preview to apply transformations to the image before Tesseract processing (e.g. thresholding)

- Ensure you are using the latest version of Tesseract-OCR with the correct support libraries
		