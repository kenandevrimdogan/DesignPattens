Behavioral Patterns(Davranışsal Tasarım Kalıbı): Nesnelerin birbirleri ile ilişkisini düzenleyen desendir.

Bilinmesi Gerekenler:
AbstractClass: Şablon algoritmanın tanımlanacağı soyut sınıf.
ConcreteClass: Şablon algoritmanın adımlarını gerçekleştirecek olan gerçek sınıf.

Bkz: https://refactoring.guru/design-patterns/behavioral-patterns)

- Strategy Pattern: Bir işi yapabilecek birden fazla algoritma yapısı kurmayı sağlar. Böylece var olan sınıfımız üzerinde değişiklik yapmadan sistemimizi geliştirmeyi sağlar. (Open-Closed)
- Template Pattern: Şablon bir algoritmamızın soyut(abstract) bir sınıfa implemente edilip, sorumlulukların alt sınıflara dağıtılması işlemidir.
- Command Pattern: Algoritma listesini tutarak, sıra ile algoritmaları çalıştırmayı sağlar.
- Observer Pattern: Tasarlanmış olan sistem içerisinde, değişimi takip etmeyi ve takip sonrası işlemleri gerçekleştirmeyi sağlar.
- Chain Of Responsibility Pattern: Bir birini takip eden algoritmaları handler ederek sırayla gerçekleştirir.

DesignPattens Projeleri
- Strategy Pattern (WebApp.Strategy): Giriş yapan üyenin çalışma esnasında(runtime) CRUD işlemlerin gerçekleştiği Database seçme ve güncellemesini sağlar.
- Template Pattern (WebApp.Template): Giriş yapan üyenin tipine göre diğer kullanıcıların bilgilerinin görme yetkisi sağlar.
- Command Pattern (WebApp.Command): Ürün listesini sıra ile "Excel" ve "Pdf" dosyası türüne çevirir.
- Observer Pattern (WebApp.Observer): Üye olan kullanıcıya console kısmına log, indirim ve hoşgeldiniz e-postası atar.
- Chain Of Responsibility Pattern (WebApp.ChainOfResponsibility): Önce excel dosyası haline getirilen ürünler ardından zip yapılıp, e-posta olarak gönderilir.


Kaynaklar
- https://github.com/Fcakiroglu16
- https://www.gokhan-gokalp.com/
- https://www.argenova.com.tr/
