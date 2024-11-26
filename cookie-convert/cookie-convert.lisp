(require :uiop)
(require :shasht)

(defvar cookies-txt)

(with-open-file (input "cookies.JDownloader.txt")
  (let ((cookies (make-string (file-length input))))
    (read-sequence cookies input)
    (setf cookies-txt (uiop:split-string cookies :separator uiop:+lf+))))

(shasht:write-json (loop for cookie in cookies-txt
                         for split = (uiop:split-string cookie :separator '(#\Tab))
                         if (= 7 (length split))
                           collect (list :object-alist
                                         (cons "domain" (nth 0 split))
                                         (cons "path" (nth 2 split))
                                         (cons "name" (nth 5 split))
                                         (cons "value" (nth 6 split))
                                         (cons "expirationDate" (parse-integer (nth 4 split)))
                                         (cons "hostOnly" nil)
                                         (cons "secure" t)
                                         (cons "httpOnly" nil)
                                         (cons "session" nil)
                                         (cons "partitionKey" :null)
                                         (cons "storeId" :null))))
