# 2021 메타버스 개발자 경진대회 참가작
### Circuit_Simulator
- 메타버스 환경의 가상 전자회로 시뮬레이터

<p align="center">
  <img src="https://user-images.githubusercontent.com/74342121/148024659-9b49baba-d2a4-4c65-a5c2-ae2f5515fce8.jpg" width="250" height="350">
</p>

<br/>

## 개발
### 개발환경
- OS : Windows 10
- CPU : Intel(R) Xeon(R) CPU E3-1245 v6 @ 3.70GHz 3.70 GHz
- GPU : GTX 1080 Ti
- Oculus Developer Hub : 1.11.1
- Unity Hub : 2.4.5
- Unity : 2020.03.19.f1
- Platform : Android

### 개발언어
- Runtime : C++
- Unity Scripting API : C#

<br/>

## 시스템 구성 및 아키텍처
### 시스템 구성
- 본 시스템은 APK 확장자 파일을 통해 VR 기기인 Oculus Quest 2에 설치 및 실행 가능
- Unity를 이용하여 개발한 APK 확장자 파일을 ODH(Oculus Developer Hub)에서 VR 기기와 연결해 설치한 후 사용자가 VR 기기에서 프로그램 확인

### 아키텍처
- 시스템은 Unity를 기반으로 개발을 진행한다.
- VR에서 실행할 수 있는 환경을 만들기 위해 Unity에서 제공하는 XR Toolkit을 사용한다. 환경을 구축한 후 제작하고 Build를 진행할 때 사용하는 플랫폼은 Android이다. 
- Build를 진행하면 APK 파일이 생성된다. 이 파일을 ODH에 업로드하면 ODH와 연동된 VR 기기에 설치가 진행되고 이를 실행하면 VR 기기에서 프로그램이 작동된다.

### 절차
- 회로도 구성 : Oculus Quest 2 장비를 이용하여 사용자가 브레드보드에 소자를 선택하여 회로를 구성한다.
- 회로도 배열 전환 및 저장 : 사용자가 선택한 회로도와 소자의 쌍을 배열로 저장한다.
- 회로 연결 상태 판단 : 사용자가 “시뮬레이션 시작” 버튼을 선택하게 되면 회로의 연결이 잘 되어있는지 판단을 진행한다.
- 회로 시뮬레이션 시작 : 회로의 연결이 잘 되어있으면 3D 화면상 브레드보드에 사용자가 연결한 회로가 보이게끔 한다. 
