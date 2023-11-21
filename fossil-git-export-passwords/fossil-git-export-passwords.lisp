;; (replace-tokens-in-repo-directory "~/fossils/git-mirror" "ghp_newtoken")

(require "asdf")
(require "sqlite")
(import 'sqlite:connect)
(import 'sqlite:disconnect)
(import 'sqlite:execute-non-query)
(import 'sqlite:execute-single)
(require "uiop")
(import 'uiop:subdirectories)

(defun current-autopush-setting (db)
  (execute-single db "SELECT value FROM mconfig WHERE key='autopush'"))

(defun mirror-state-db-path (base-path)
  (merge-pathnames ".mirror_state/db" base-path))

(defun mirror-state-db (base-path)
  (connect (mirror-state-db-path base-path)))

(defun push-url-with-new-token (url token)
  (concatenate 'string
               (subseq url 0 (search "ghp_" url))
               token
               (subseq url (position #\@ url))))

(defun replace-autopush-setting (db url)
  (execute-non-query db
                     "REPLACE INTO mconfig(key,value) VALUES('autopush', ?)"
                     url))

(defun replace-tokens-in-repo (repo-path new-token)
  (let ((db (mirror-state-db repo-path)))
    (replace-autopush-setting db (push-url-with-new-token (current-autopush-setting db) new-token))
    (disconnect db)))

(defun replace-tokens-in-repo-directory (base-path new-token)
  (dolist (repo (subdirectories base-path))
    (replace-tokens-in-repo repo new-token)))

(defun print-current-urls ()
  (dolist (repo (subdirectories "."))
    (let ((db (mirror-state-db repo)))
      (format t "~a~%" (current-autopush-setting db))
      (disconnect db))))
