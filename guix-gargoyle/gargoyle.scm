(use-modules (guix packages)
             (guix git-download)
             (guix build-system cmake)
             (guix licenses)
             (gnu packages fontutils)
             (gnu packages image)
             (gnu packages qt)
             (gnu packages pkg-config)
             (gnu packages sdl))

(let ((commit "a02c2095978db492f02a0b74157d48f51edde9fa")
      (revision "1"))
  (package
    (name "gargoyle")
    (version (git-version "2019.1.1" revision commit))
    (source
     (origin
       (method git-fetch)
       (uri (git-reference
             (url "https://github.com/garglk/garglk")
             (commit commit)))
       (file-name (git-file-name name version))
       (sha256
        (base32 "008n2gq0if4mkvli7dcz9m4wwr5y4xlmipm2axrm0lsg29namr8y"))))
    (build-system cmake-build-system)
    (arguments '(#:tests? #f))
    (native-inputs (list pkg-config))
    (inputs (list freetype libjpeg-turbo qtbase-5 sdl2-mixer fontconfig))
    (home-page "http://ccxvii.net/gargoyle/")
    (synopsis "An interactive fiction player")
    (description
     "Gargoyle is an IF player that supports all the major interactive fiction formats.

Most interactive fiction is distributed as portable game files. These portable
game files come in many formats. In the past, you used to have to download a
separate player (interpreter) for each format of IF you wanted to play.

Gargoyle is based on the standard interpreters for the formats it
supports.")
    (license gpl2)))
