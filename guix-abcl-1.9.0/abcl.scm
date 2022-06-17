(use-modules (guix download)
             (guix packages)
             (gnu packages java))

(define abcl-1.9.0
  (package
   (inherit abcl)
   (version "1.9.0")
   (source
    (origin
     (method url-fetch)
     (uri (string-append "https://abcl.org/releases/"
                         version "/abcl-src-" version ".tar.gz"))
     (sha256
      (base32
       "0scqq5c7201xhp0g6i4y3m2nrk6l5any1nisiscbsd48ya25qax1"))
     (patches
      '("abcl-fix-build-xml.patch"))))))

(packages->manifest (list abcl-1.9.0))
