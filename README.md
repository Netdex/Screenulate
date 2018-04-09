# Screenulate
`This project is a work-in-progress.`

Translate screenshots on the fly, so I can read my visual novels.
![screenshot](https://i.imgur.com/lko3pAi.png)

Uses Tesseract-OCR for OCR, then opens Google Translate in a web view with the text.

Currently only supports `ja -> en`.

## Usage
```
ALT+T           Enter screen capture mode
ALT+SHIFT+T     Reuse previous screen capture region
```

In screen capture mode, click and drag to select a region to read (will appear as a red rectangle). The previous region you selected will appear as a green rectangle. In screen capture mode,

```
ESC             Exit screen capture mode
SPACE           Reuse previous screen capture region
```

After you complete the capture, the source text and translated text will appear in Screenulate.
