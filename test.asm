//require 100301:st=6 rk=18 e3=-1 e4=3
//require 107101:st=5 rk=19
//require 102801:st=6 rk=18 e3=-1 e4=-1
//require 106801:st=5
//require 110701:st=5
//require 401311401:
mov 100301,怜
mov 107101,克
mov 102801,咲恋
mov 106801,拉比林斯
mov 110701,插班生碧
wait 867; press 拉比林斯,5; // lframe 867
wait 1302; press 克,5; // lframe 1103
//bossub
wait 1776; press 怜,5; // lframe 1185
wait 2289; press 克,5; // lframe 1401
wait 2809; press 怜,5; // lframe 1692
wait 3156; press 咲恋,5; // lframe 1742
//bossub
wait 3828; press 克,5; // lframe 1975
wait 4057; press 插班生碧,5; // lframe 1975
wait 4404; press 克,5; // lframe 2081
wait 4819; press 怜,5; // lframe 2267
wait 5421; press 拉比林斯,5; // lframe 2572
//bossub
wait 5931; press 咲恋,5; // lframe 2720
wait 6207; press 克,5; // lframe 2720
wait 6551; press 怜,5; // lframe 2835
wait 6976; press 克,5; // lframe 2963
wait 7719; press 插班生碧,5; // lframe 3477
//bossub
wait 8179; press 怜,5; // lframe 3533
wait 8527; press 克,5; // lframe 3584
wait 8849; press 克,5; // lframe 3677
wait 9462; press 咲恋,5; // lframe 4061
wait 9738; press 怜,5; // lframe 4061
waitl 4206;
in_tp 401311401,bosstp;
lt bosstp,1000,正常;
jc label_正常线,正常;
wait 10402; press 拉比林斯,5; // lframe 4265
wait 10601; press 克,5; // lframe 4265
wait 11104; press 克,5; // lframe 4539
wait 11365; press 怜,5; // lframe 4571
wait 12018; press 插班生碧,5; // lframe 4927
//bossub
wait 12543; press 咲恋,5; // lframe 5048
wait 12911; press 克,5; // lframe 5140
wait 13186; press 怜,5; // lframe 5186
wait 13543; press 克,5; // lframe 5246
end
label_正常线:
waitl 4265; press 拉比林斯,5; // lframe 4265
waitl 4284; press 克,5; // lframe 4284
waitl 4558; press 克,5; // lframe 4558
waitl 4674; press 怜,5; // lframe 4674
waitl 4927; press 插班生碧,5; // lframe 4927
//bossub
waitl 5066; press 咲恋,5; // lframe 5066
waitl 5140; press 克,5; // lframe 5140
~
//require 110301:st=3 rk=14
//require 112001:st=3 rk=18
//require 101401:st=3 rk=11
//require 108801:st=3 rk=12 ub=181
//require 108401:st=3 rk=12 ub>153 s2=167
//require 401311401:
mov 110301,水电
mov 112001,春黑
mov 101401,香橙
mov 108801,春田
mov 108401,圣千
wait 845; press 水电,5; // lframe 845
wait 1471; press 春田,5; // lframe 1207
wait 1676; press 香橙,5; // lframe 1213
wait 1863; press 春黑,5; // lframe 1213
//bossub
wait 2455; press 春黑,5; // lframe 1381
wait 2810; press 圣千,5; // lframe 1475
wait 3180; press 香橙,5; // lframe 1739
wait 3367; press 春田,5; // lframe 1739
wait 3567; press 春黑,5; // lframe 1740
//bossub
wait 4033; press 春黑,5; // lframe 1782
wait 4486; press 春黑,5; // lframe 1974
wait 5059; press 香橙,5; // lframe 2286
wait 5246; press 春黑,5; // lframe 2286
//bossub
wait 5792; press 春黑,5; // lframe 2408
wait 6053; press 春田,5; // lframe 2408
wait 6385; press 春黑,5; // lframe 2541
wait 6758; press 香橙,5; // lframe 2653
wait 7048; press 春黑,5; // lframe 2756
wait 7559; press 春黑,5; // lframe 3006
//bossub
wait 8105; press 春田,5; // lframe 3128
wait 8332; press 春黑,5; // lframe 3156
wait 8819; press 春黑,5; // lframe 3382
wait 9203; press 圣千,5; // lframe 3505
wait 9336; press 香橙,5; // lframe 3532
wait 9529; press 春黑,5; // lframe 3538
~
//require 106101:st=5 rk=19
//require 110401:st=5 rk=19
//require 107101:st=5 rk=19
//require 103401:st=6 rk=14
//require 106801:st=5 rk=19
//require 401311402:
mov 106101,sb511
mov 110401,水狼
mov 107101,克
mov 103401,黄骑
mov 106801,拉比林斯
wait 887; press 克,5; // lframe 887
wait 1153; press 拉比林斯,5; // lframe 924
wait 1802; press 克,5; // lframe 1374
//bossub
wait 2217; press 黄骑,5; // lframe 1379
wait 2634; press 水狼,5; // lframe 1448
wait 2910; press sb511,5; // lframe 1448
wait 3579; press 水狼,5; // lframe 1830
wait 3879; press 克,5; // lframe 1854
wait 4360; press 克,5; // lframe 2106
//bossub
wait 5389; press 拉比林斯,5; // lframe 2725
wait 5700; press 水狼,5; // lframe 2837
wait 5981; press 克,5; // lframe 2842
wait 6332; press 克,5; // lframe 2964
//bossub
wait 7200; press 黄骑,5; // lframe 3422
wait 7660; press 水狼,5; // lframe 3534
wait 8004; press 克,5; // lframe 3602
wait 8362; press 克,5; // lframe 3731
wait 8666; press 水狼,5; // lframe 3806
wait 9213; press sb511,5; // lframe 4077
waitl 4330;
in_tp 401311402,bosstp;
lt bosstp,1000,正常;
jc label_正常线,正常;
waitl 4488; press 拉比林斯,5;
waitl 4567; press 克,5;
waitl 4882; press 水狼,5;
waitl 4996; press 黄骑,5;
waitl 5077; press 克,5;
waitl 5353; press 克,5;
end
label_正常线:
waitl 4492; press 拉比林斯,5;
waitl 4561; press 克,5;
waitl 4831; press 水狼,5;
waitl 4996; press 黄骑,5;
waitl 5167; press 克,5;
waitl 5290; press 克,5;
waitl 5396; press 克,3;
~
//require 106101:st=5 rk=19
//require 110401:st=4 rk=19
//require 107101:st=5 rk=19
//require 103401:st=6 rk>14
//require 106801:st=5
//require 401311402:
mov 106101,sb511
mov 110401,水狼
mov 107101,克
mov 103401,黄骑
mov 106801,拉比林斯
wait 924; press 拉比林斯,5; // lframe 924
wait 1451; press 克,5; // lframe 1252
wait 1786; press 克,5; // lframe 1358
//bossub
wait 2217; press 黄骑,5; // lframe 1379
wait 2634; press sb511,5; // lframe 1448
wait 3037; press 水狼,5; // lframe 1564
wait 3603; press 克,5; // lframe 1854
wait 3925; press 水狼,5; // lframe 1947
wait 4360; press 克,5; // lframe 2106
//bossub
wait 5389; press 拉比林斯,5; // lframe 2725
wait 5700; press 水狼,5; // lframe 2837
wait 5981; press 克,5; // lframe 2842
wait 6332; press 克,5; // lframe 2964
//bossub
wait 7200; press 黄骑,5; // lframe 3422
wait 7660; press 水狼,5; // lframe 3534
wait 8004; press 克,5; // lframe 3602
wait 8362; press 克,5; // lframe 3731
wait 8666; press 水狼,5; // lframe 3806
wait 9213; press sb511,5; // lframe 4077
//bossub
wait 10096; press 拉比林斯,5; // lframe 4492
wait 10364; press 克,5; // lframe 4561
wait 10863; press 水狼,5; // lframe 4831
wait 11304; press 黄骑,5; // lframe 4996
wait 11823; press 克,5; // lframe 5167
wait 12175; press 克,5; // lframe 5290
wait 12510; press 克,5; // lframe 5396
//bossub
~
//require 107101:st=5
//require 102801:st=6 rk=9
//require 106801:st=5
//require 110701:
//require 110001:st=5 rk=19
//require 401311403:
mov 107101,克
mov 102801,咲恋
mov 106801,拉比林斯
mov 110701,插班生碧
mov 110001,水暴
waitl 483; press 克,5; // lframe 483
waitl 714; press 拉比林斯,5; // lframe 714
waitl 1154; press 克,5; // lframe 1154
waitl 1263; press 克,5; // lframe 1263
waitl 1407; press 咲恋,5; // lframe 1407
//bossub
waitl 1836; press 克,5; // lframe 1836
waitl 1937; press 插班生碧,5; // lframe 1937
waitl 1942; press 克,5; // lframe 1942
waitl 2052; press 水暴,5; // lframe 2052
waitl 2136; press 水暴,5; // lframe 2136
waitl 2351; press 拉比林斯,5; // lframe 2351
waitl 2495; press 咲恋,5; // lframe 2495
waitl 2515; press 克,5; // lframe 2515
//bossub
waitl 2789; press 克,5; // lframe 2789
waitl 3363; press 克,5; // lframe 3363
waitl 3453; press 插班生碧,5; // lframe 3453
waitl 3480; press 咲恋,5; // lframe 3480
waitl 3481; press 克,5; // lframe 3481
waitl 3486; press 水暴,5; // lframe 3486
//bossub
waitl 3999; press 克,5; // lframe 3999
waitl 4083; press 拉比林斯,5; // lframe 4083
waitl 4257; press 克,5; // lframe 4257
waitl 4400; press 咲恋,5; // lframe 4400
waitl 4897; press 克,5; // lframe 4897
//bossub
waitl 4977; press 插班生碧,5; // lframe 4977
waitl 5003; press 克,5; // lframe 5003
waitl 5188; press 水暴,5; // lframe 5188
~
//require 110301:st=3 rk=14
//require 112001:st=5 rk=18 e1=5 e2=5 e3=-1 e4=5 e5=5 e6=5 ub=177 s1=177 s2=177 ex=177 lv=177
//require 101401:
//require 108801:st=3 s1>174
//require 100801:rk>12
//require 401311403:
mov 110301,水电
mov 112001,春黑
mov 101401,香橙
mov 108801,春田
mov 100801,雪
wait 714; press 香橙,5; // lframe 714
wait 901; press 春黑,5; // lframe 714
wait 1326; press 水电,5; // lframe 878
//bossub
wait 2135; press 春田,5; // lframe 1224
wait 2514; press 春黑,5; // lframe 1404
wait 2838; press 春黑,5; // lframe 1467
wait 3235; press 香橙,5; // lframe 1603
wait 3537; press 春黑,5; // lframe 1718
wait 3830; press 雪,5; // lframe 1750
wait 4172; press 春田,5; // lframe 1881
wait 4593; press 春黑,5; // lframe 2103
wait 4877; press 春黑,5; // lframe 2126
//bossub
wait 5801; press 香橙,5; // lframe 2590
wait 5993; press 春黑,5; // lframe 2595
wait 6628; press 春黑,5; // lframe 2969
wait 7390; press 香橙,5; // lframe 3470
wait 7588; press 春黑,5; // lframe 3481
wait 7889; press 春田,5; // lframe 3521
//bossub
wait 8578; press 春黑,5; // lframe 3812
wait 9046; press 春黑,5; // lframe 4019
wait 9669; press 春黑,5; // lframe 4381
wait 10000; press 雪,5; // lframe 4451
wait 10323; press 香橙,5; // lframe 4563
wait 10555; press 春黑,5; // lframe 4608
wait 11151; press 春田,5; // lframe 4943
wait 11435; press 春黑,5; // lframe 5028
~
//require 110301:st=3 rk=14
//require 112001:st=5 rk=18 e1=5 e2=5 e3=-1 e4=5 e5=5 e6=5 ub=177 s1=177 s2=177 ex=177 lv=177
//require 101401:
//require 108801:st=3 lv=174
//require 100801:rk>12
//require 401311403:
mov 110301,水电
mov 112001,春黑
mov 101401,香橙
mov 108801,春田
mov 100801,雪
wait 714; press 水电,5; // lframe 714
wait 978; press 香橙,5; // lframe 714
wait 1165; press 春黑,5; // lframe 714
//bossub
wait 2111; press 春田,5; // lframe 1200
wait 2499; press 春黑,5; // lframe 1389
wait 2838; press 春黑,5; // lframe 1467
wait 3339; press 春田,5; // lframe 1707
wait 3586; press 雪,5; // lframe 1755
wait 3877; press 香橙,5; // lframe 1835
wait 4079; press 春黑,5; // lframe 1850
wait 4593; press 春黑,5; // lframe 2103
wait 4964; press 春黑,5; // lframe 2213
wait 5296; press 春田,5; // lframe 2284
//bossub
wait 6005; press 春黑,5; // lframe 2595
wait 6493; press 香橙,5; // lframe 2822
wait 6833; press 春黑,5; // lframe 2975
wait 7098; press 春田,5; // lframe 2979
wait 7560; press 春黑,5; // lframe 3242
wait 8261; press 雪,5; // lframe 3682
wait 8520; press 春田,5; // lframe 3730
wait 8723; press 春黑,5; // lframe 3734
//bossub
wait 9307; press 香橙,5; // lframe 3858
wait 9597; press 春黑,5; // lframe 3961
wait 10278; press 春黑,5; // lframe 4381
wait 10682; press 春田,5; // lframe 4524
wait 11120; press 春黑,5; // lframe 4763
~
//require 105901:st=6 rk=14 
//require 107001:st=3 rk=14 ub<161
//require 110101:st=3
//require 108401:st=3 rk=12
//require 101001:st=6 rk=12
//require 401311404:
mov 105901,普白
mov 107001,似似花
mov 110101,水魅魔
mov 108401,圣千
mov 101001,真步
waitl 1113; presson 普白;
waitl 1488; presson 似似花;
waitl 1602; press 水魅魔,5; 
waitl 1682; press 圣千,5; 
presson 真步;
waitl 2079; presson 水魅魔; 
waitl 2079; presson 圣千; 
waitl 2223;pressoff 圣千;
waitl 2238; press 圣千,5; 
waitl 2281; press 圣千,5; 
waitl 2324; press 圣千,5; 
waitl 2367; press 圣千,5; 
waitl 2410; press 圣千,5; 
waitl 2453; press 圣千,5; 
waitl 2496; press 圣千,5; 
waitl 2539; press 圣千,5; 
waitl 2582; press 圣千,5; 
waitl 2625; press 圣千,5; 
waitl 2668; presson 圣千; 
~
//require 109301:st>3
//require 107001:st=3 rk=14 ub<161
//require 112101:st<4 rk=12
//require 108401:st=3 rk=12
//require 101001:st=6 rk=12
//require 401311404:
mov 109301,露
mov 107001,似似花
mov 112101,春女仆
mov 108401,圣千
mov 101001,真步
waitl 100; presson 春女仆;
waitl 1833; press 似似花,5; 
waitl 1950; presson 圣千; 
presson 真步; 
presson 露; 
pressoff 春女仆;
waitl 2079; 
presson 春女仆; 
presson 似似花;
waitl 2222;  pressoff 圣千;
waitl 2238;  press 圣千,5;
waitl 2281;  press 圣千,5;
waitl 2324; press 圣千,5; 
waitl 2367; press 圣千,5; 
waitl 2410; press 圣千,5; 
waitl 2453; press 圣千,5; 
waitl 2496; press 圣千,5; 
waitl 2539; press 圣千,5; 
waitl 2582; press 圣千,5; 
waitl 2625; press 圣千,5; 
waitl 2668; presson 圣千; 
~
//require 105901:st=6 rk>14
//require 107001:st=4 rk=14
//require 112001:st=5 rk=19
//require 101001:st=6 rk=12 lv>164
//require 111101:st=4 rk=14
//require 401311405:
mov 105901,普白
mov 107001,似似花
mov 112001,春黑
mov 101001,真步
mov 111101,万圣XCW
wait 1197; press 似似花,5; // lframe 1197
wait 1432; press 普白,5; // lframe 1221
wait 1797; press 万圣XCW,5; // lframe 1238
wait 2197; press 春黑,5; // lframe 1397
wait 2539; press 似似花,5; // lframe 1478
wait 2993; press 真步,5; // lframe 1721
wait 3404; press 万圣XCW,5; // lframe 1742
wait 3810; press 似似花,5; // lframe 1907
wait 4075; press 普白,5; // lframe 1961
wait 4537; press 春黑,5; // lframe 2075
wait 4985; press 真步,5; // lframe 2262
wait 5375; press 万圣XCW,5; // lframe 2262
wait 5616; press 似似花,5; // lframe 2262
//bossub
wait 6338; press 普白,5; // lframe 2383
wait 6837; press 万圣XCW,5; // lframe 2534
wait 7078; press 春黑,5; // lframe 2534
wait 7376; press 真步,5; // lframe 2571
wait 7766; press 似似花,5; // lframe 2571
wait 8033; press 似似花,5; // lframe 2627
wait 8244; press 万圣XCW,5; // lframe 2627
wait 8485; press 春黑,5; // lframe 2627
wait 9006; press 真步,5; // lframe 2887
wait 9396; press 普白,5; // lframe 2887
wait 9744; press 万圣XCW,5; // lframe 2887
wait 10106; press 似似花,5; // lframe 3008
wait 10377; press 春黑,5; // lframe 3068
wait 10938; press 万圣XCW,5; // lframe 3368
wait 11179; press 真步,5; // lframe 3368
wait 11569; press 似似花,5; // lframe 3368
wait 11780; press 普白,5; // lframe 3368
wait 12219; press 春黑,5; // lframe 3459
wait 12657; press 万圣XCW,5; // lframe 3636
//bossub
wait 13325; press 真步,5; // lframe 3673
wait 13715; press 似似花,5; // lframe 3673
wait 13999; press 万圣XCW,5; // lframe 3746
wait 14240; press 普白,5; // lframe 3746
wait 14651; press 春黑,5; // lframe 3809
wait 15119; press 似似花,5; // lframe 4016
wait 15330; press 万圣XCW,5; // lframe 4016
wait 15571; press 春黑,5; // lframe 4016
wait 15832; press 真步,5; // lframe 4016
wait 16438; press 万圣XCW,5; // lframe 4232
wait 16752; press 普白,5; // lframe 4305
wait 17238; press 春黑,5; // lframe 4443
wait 17499; press 似似花,5; // lframe 4443
wait 17784; press 真步,5; // lframe 4517
wait 18188; press 万圣XCW,5; // lframe 4531
wait 18605; press 似似花,5; // lframe 4707
wait 18942; press 普白,5; // lframe 4833
wait 19471; press 春黑,5; // lframe 5014
wait 19732; press 万圣XCW,5; // lframe 5014
wait 20011; press 真步,5; // lframe 5052
//bossub
wait 20801; press 似似花,5; // lframe 5062
wait 21074; press 万圣XCW,5; // lframe 5124
wait 21394; press 普白,5; // lframe 5203
wait 21892; press 真步,5; // lframe 5353
wait 22297; press 万圣XCW,5; // lframe 5368
wait 22558; press 春黑,5; // lframe 5388
~
//require 105901:st=6 rk>14
//require 107001:st=4 rk=14 lv=179
//require 101201:st=6 rk=19 e4=-1
//require 108401:st=3 rk=12 ub=1 lv>166
//require 101001:st=6 rk=12
//require 401311405:
mov 105901,普白
mov 107001,似似花
mov 101201,初音
mov 108401,圣千
mov 101001,真步
wait 1020; press 普白,5; // lframe 1020
wait 1368; press 似似花,5; // lframe 1020
wait 1807; press 圣千,5; // lframe 1248
wait 1935; press 似似花,5; // lframe 1270
wait 2146; press 初音,5; // lframe 1270
wait 2658; press 真步,5; // lframe 1422
wait 3219; press 圣千,5; // lframe 1593
wait 3352; press 似似花,5; // lframe 1620
wait 3563; press 初音,5; // lframe 1620
wait 3966; press 普白,5; // lframe 1663
//bossub
wait 4747; press 似似花,5; // lframe 1706
wait 4958; press 初音,5; // lframe 1706
wait 5318; press 圣千,5; // lframe 1706
wait 5424; press 真步,5; // lframe 1706
wait 5814; press 普白,5; // lframe 1706
wait 6423; press 真步,5; // lframe 1967
wait 6813; press 似似花,5; // lframe 1967
wait 7024; press 初音,5; // lframe 1967
wait 7429; press 圣千,5; // lframe 2012
wait 7562; press 普白,5; // lframe 2039
wait 8200; press 真步,5; // lframe 2329
wait 8590; press 似似花,5; // lframe 2329
wait 8801; press 初音,5; // lframe 2329
wait 9200; press 圣千,5; // lframe 2368
//bossub
wait 9723; press 普白,5; // lframe 2395
wait 10127; press 真步,5; // lframe 2451
wait 10517; press 似似花,5; // lframe 2451
wait 10728; press 初音,5; // lframe 2451
wait 11088; press 圣千,5; // lframe 2451
wait 11288; press 普白,5; // lframe 2545
wait 11659; press 真步,5; // lframe 2568
wait 12049; press 似似花,5; // lframe 2568
wait 12260; press 圣千,5; // lframe 2568
wait 12393; press 初音,5; // lframe 2595
wait 12886; press 普白,5; // lframe 2728
wait 13234; press 似似花,5; // lframe 2728
wait 13445; press 圣千,5; // lframe 2728
wait 13551; press 真步,5; // lframe 2728
wait 13941; press 初音,5; // lframe 2728
//bossub
wait 14737; press 似似花,5; // lframe 2774
wait 14948; press 圣千,5; // lframe 2774
wait 15080; press 初音,5; // lframe 2800
wait 15441; press 普白,5; // lframe 2801
wait 15789; press 真步,5; // lframe 2801
wait 16356; press 似似花,5; // lframe 2978
wait 16571; press 圣千,5; // lframe 2982
wait 16704; press 初音,5; // lframe 3009
wait 17101; press 普白,5; // lframe 3046
wait 17453; press 真步,5; // lframe 3050
wait 17946; press 似似花,5; // lframe 3153
wait 18157; press 圣千,5; // lframe 3153
wait 18290; press 初音,5; // lframe 3180
wait 18685; press 真步,5; // lframe 3215
wait 19075; press 普白,5; // lframe 3215
//bossub
wait 19895; press 普白,5; // lframe 3297
wait 20243; press 似似花,5; // lframe 3297
wait 20454; press 初音,5; // lframe 3297
wait 20814; press 圣千,5; // lframe 3297
wait 20920; press 真步,5; // lframe 3297
wait 21351; press 圣千,5; // lframe 3338
wait 21477; press 真步,5; // lframe 3358
wait 21867; press 似似花,5; // lframe 3358
wait 22078; press 初音,5; // lframe 3358
wait 22445; press 普白,5; // lframe 3365
wait 22924; press 真步,5; // lframe 3496
wait 23314; press 普白,5; // lframe 3496
wait 23662; press 似似花,5; // lframe 3496
wait 23873; press 初音,5; // lframe 3496
wait 24233; press 圣千,5; // lframe 3496
wait 24366; press 真步,5; // lframe 3523
wait 24775; press 似似花,5; // lframe 3542
wait 25026; press 初音,5; // lframe 3582
wait 25461; press 圣千,5; // lframe 3657
wait 25594; press 普白,5; // lframe 3684
wait 25942; press 真步,5; // lframe 3684
wait 26332; press 似似花,5; // lframe 3684
wait 26567; press 初音,5; // lframe 3708
wait 27037; press 圣千,5; // lframe 3818
wait 27170; press 真步,5; // lframe 3845
wait 27560; press 普白,5; // lframe 3845
wait 27908; press 似似花,5; // lframe 3845
wait 28119; press 初音,5; // lframe 3845
wait 28610; press 圣千,5; // lframe 3976
//bossub
wait 29106; press 似似花,5; // lframe 3976
wait 29317; press 初音,5; // lframe 3976
wait 29677; press 真步,5; // lframe 3976
wait 30067; press 普白,5; // lframe 3976
wait 30439; press 圣千,5; // lframe 4000
wait 30548; press 真步,5; // lframe 4003
wait 30938; press 似似花,5; // lframe 4003
wait 31149; press 初音,5; // lframe 4003
wait 31538; press 普白,5; // lframe 4032
wait 31918; press 圣千,5; // lframe 4064
wait 32043; press 真步,5; // lframe 4083
wait 32433; press 似似花,5; // lframe 4083
wait 32644; press 初音,5; // lframe 4083
wait 33083; press 圣千,5; // lframe 4162
wait 33260; press 普白,5; // lframe 4233
wait 33608; press 真步,5; // lframe 4233
wait 33998; press 似似花,5; // lframe 4233
wait 34209; press 初音,5; // lframe 4233
wait 34655; press 普白,5; // lframe 4319
wait 35003; press 似似花,5; // lframe 4319
wait 35214; press 初音,5; // lframe 4319
wait 35574; press 圣千,5; // lframe 4319
wait 35680; press 真步,5; // lframe 4319
wait 36097; press 似似花,5; // lframe 4346
wait 36308; press 初音,5; // lframe 4346
wait 36668; press 真步,5; // lframe 4346
wait 37058; press 普白,5; // lframe 4346
wait 37406; press 圣千,5; // lframe 4346
//bossub
wait 37921; press 似似花,5; // lframe 4365
wait 38132; press 初音,5; // lframe 4365
wait 38492; press 真步,5; // lframe 4365
wait 38886; press 普白,5; // lframe 4369
wait 39235; press 圣千,5; // lframe 4370
wait 39385; press 似似花,5; // lframe 4414
wait 39601; press 初音,5; // lframe 4419
wait 39961; press 真步,5; // lframe 4419
wait 40351; presson 圣千; // lframe 4419
presson 真步;
presson 初音;
presson 似似花;
presson 普白;