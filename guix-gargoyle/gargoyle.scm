(use-modules (guix packages)
             (guix git-download)
             (guix build-system cmake)
             ((guix licenses)
              #:prefix license:)
             (gnu packages fontutils)
             (gnu packages image)
             (gnu packages qt)
             (gnu packages pkg-config)
             (gnu packages sdl)
             (gnu packages glib)
             (gnu packages speech))

(define gargoyle
  (let ((commit "bde657b41e67f56e9f2bf58f48eef56ac002030f")
        (revision "1"))
    (package
      (name "gargoyle")
      (version (git-version "2023.1" revision commit))
      (source
       (origin
         (method git-fetch)
         (uri (git-reference
               (url "https://github.com/garglk/garglk")
               (commit commit)))
         (file-name (git-file-name name version))
         (sha256
          (base32 "05jsk25dn4zss068v96nqvr8w4w4pabqcbjaaln98gvrmy1498rv"))))
      (build-system cmake-build-system)
      (arguments
       '(#:configure-flags '("-DWITH_QT6=true")
         #:tests? #f))
      (native-inputs (list pkg-config))
      (inputs (list freetype
                    libjpeg-turbo
                    qtbase
                    sdl2-mixer
                    fontconfig
                    glib
                    speech-dispatcher))
      (home-page "http://ccxvii.net/gargoyle/")
      (synopsis "An interactive fiction player")
      (description
       "Gargoyle is an IF player that supports all the major interactive fiction formats.

Most interactive fiction is distributed as portable game files. These portable
game files come in many formats. In the past, you used to have to download a
separate player (interpreter) for each format of IF you wanted to play.

Gargoyle is based on the standard interpreters for the formats it
supports.")
      (license license:gpl2))))

gargoyle
