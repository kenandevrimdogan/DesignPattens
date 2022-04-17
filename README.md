Creational Design Patterns: Nesne yaratma mekanizmasıyla ilgilenen, uygulamamızda duruma uygun şekilde nesne yaratmaya çalışan tasarım kalıplarıdır
Behavioral Patterns(Davranışsal Tasarım Kalıbı): Nesnelerin birbirleri ile ilişkisini düzenleyen desendir.

Bilinmesi Gerekenler:
- Single Responsibility: Tek sorumluluğa sahip olmak.
- Open Closed: Gelişme açık - değişime kapalı.
- Liskov Substitution Principle: Alt sınıflardan oluşan nesnelerin üst sınıfın nesneleri ile yer değiştirdikleri zaman, aynı davranışı sergilemesi.
- Loose Coupling - Gevşek Bağlılık Prensibi
- Interface Segregation Principle: Arayüzlerin ayrıştırılması.
- Dependency Inversion Principle: Bağlılığı Tersine Çevirme. Yüksek seviye modüller, düşük seviye modüllere bağlı olmamalıdır.
- Dependency Injection: Bağlılığı enjecekte etmeyi sağlar. (Constructor Injection, Property Injection, Method Injection)

- Inversion of control: Ioc ile Uygulama içerisindeki obje instance’larının yönetimi sağlanarak, bağımlılıklarını en aza indirgemek amaçlanmaktadır.

Immobility: Geliştirilen modüllerin tekrar kullanıma uygun olmaması.
Fragility: Yapılacak olan bir değişikliğin, başka kısımları etkilemesi.
Extendability: Esneklik.

AbstractClass: Şablon algoritmanın tanımlanacağı soyut sınıf.
ConcreteClass: Şablon algoritmanın adımlarını gerçekleştirecek olan gerçek sınıf.

- Behavioral Patterns
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

---------------------------------------------------------

-- Kaynaklar
- https://github.com/Fcakiroglu16
- https://www.gokhan-gokalp.com/
- https://www.argenova.com.tr/
- https://www.turkayurkmez.com/
- https://tugrulbayrak.medium.com/
