(def null #n)
(def void #v)
(def true #t)
(def false #f)

(def 1+ (lambda (x) (+ x 1)))

(def 1- (lambda (x) (- x 1)))

(def cadr (lambda (p) (car (cdr p))))

(define caddr (lambda (p) (car (cdr (cdr p)))))

(def not (lambda (p)
    (if p false true)))

(def < (lambda (a b)
    (not (or (= a b)
             (> a b)))))

(def <= (lambda (a b)
    (not (> a b))))

(def >= (lambda (a b)
    (not (< a b))))

(def first (lambda (ls)
    (car ls)))

(def second (lambda (ls)
    (car (cdr ls))))

(def third (lambda (ls)
    (car (cdr (cdr ls)))))

(def fourth (lambda (ls)
    (car (cdr (cdr (cdr ls))))))

(def fifth (lambda (ls)
    (car (cdr (cdr (cdr (cdr ls)))))))

(def length (lambda (ls)
    (if (null? ls)
        0
        (+ 1 (length (cdr ls))))))

(def append (lambda (l1 l2)
    (if (null? l1)
        l2
        (cons (car l1) (append (cdr l1) l2)))))

(def map (lambda (proc ls)
    (if (null? l1)
        null
        (cons (proc (car ls))
              (map proc (cdr ls))))))

(def reverse (lambda (ls)
    (def helper (lambda (ls acc)
        (if (null? ls)
            acc
            (helper (cdr ls) (cons (car ls) acc)))))
    (helper ls null)))

(def range (lambda (i)
    (def helper (lambda (n acc)
        (if (= i n)
            acc
            (helper (+ n 1) (cons n acc)))))
    (helper 0 null)))