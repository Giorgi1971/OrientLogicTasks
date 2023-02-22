# RSS feed aggregator

შექმენით აპლიკაცია, რომელიც სხვადასხვა დეველოპერული საიტებიდან მოახდენს RSS feed-ების აგრეგაციას.

მაგალითისთვის შეგიძლიათ გამოყენოთ შემდეგი RSS feed-ები:

1. [https://stackoverflow.blog/feed/](https://stackoverflow.blog/feed/)
2. [https://dev.to/feed](https://dev.to/feed)
3. [https://www.freecodecamp.org/news/rss](https://www.freecodecamp.org/news/rss) 
4. [https://martinfowler.com/feed.atom](https://martinfowler.com/feed.atom) 
5. [https://codeblog.jonskeet.uk/feed/](https://codeblog.jonskeet.uk/feed/)
6. [https://devblogs.microsoft.com/visualstudio/feed/](https://devblogs.microsoft.com/visualstudio/feed/) 
7. [https://feed.infoq.com/](https://feed.infoq.com/) 
8. [https://css-tricks.com/feed/](https://css-tricks.com/feed/) 
9. [https://codeopinion.com/feed/](https://codeopinion.com/feed/) 
10. [https://andrewlock.net/rss.xml](https://andrewlock.net/rss.xml) 
11. [https://michaelscodingspot.com/index.xml](https://michaelscodingspot.com/index.xml) 
12. [https://www.tabsoverspaces.com/feed.xml](https://www.tabsoverspaces.com/feed.xml) 

აპლიკაცია მუდმივ რეჟიმში უნდა ამოწმებდეს rss feed-ებს და ახალი სტატიის არსებობის შემთხვევაში, მონაცემთა ბაზაში უნდა ამატებდეს სტატიის შესახებ ინფორმაციას (სტატიის ბმული, სათაური, მოკლე აღწერა, ავტორი, სურათი, ტეგები, გამოქვეყნების თარიღი).

- ბაზაში სტატიის დამატების დროს, ყველა ტექსტური ველი უნდა შემოწმდეს და არსებობის შემთხვევაში უნდა წაიშალოს ჯავასკრიპტის კოდი
- ბაზაში სტატიის დამატების დროს, უნდა შემოწმდეს სათაური ან აღწერა შეიცავს თუ არა ბაზაში უკვე დამატებულ ტეგებს. თუ შეიცავს, სტატიას უნდა დაუმატოთ ყველა ასეთი ტეგი (მაგ. თუ სტატიის აღწერაში წერია: “`Last week saw the release of the third preview in the lead up to the official release of Visual Studio for Mac 17.5`" და ბაზაში არსებობს ტეგი “Visual Studio”, მაშინ ამ სტატიას ავტომატურად უნდა დაუმატოთ ტეგი “Visual Studio”). ავტომატურად დაგენერირებული ტეგების რაოდენობა არ უნდა აჭარბებდეს 5-ს. [optional]
- ერთი საიტიდან არ უნდა მოხდეს ერთნაირი სათაურის მქონე სტატიის დამატება
- თითოეული RSS feed-ის დამუშავება უნდა მოხდეს პარალელურ რეჟიმში

აპლიკაციას ასევე უნდა ჰქონდეს API, რომლითაც მომხმარებლები შეძლებენ აგრეგირებული სტატიების ნახვას (ქრონოლოგიურად, paging-ის გამოყენებით).

API-ის გამოყენებით ასევე შესაძლებელი უნდა იყოს ტეგის მიხედვით სტატიების ამოღება.
