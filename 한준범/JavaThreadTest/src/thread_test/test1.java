package thread_test;
//쓰레드 : 실, 가닥
//쓰레드는 프로세스 내부 흐름
//프로세스 내부 흐름을 쪼개서, 여러가지 쓰레드를 동시실행
//프로세스 내부 흐름을 쪼개서, 비동기적으로 여러 쓰레드를 동시 실행
//동기, 비동기 개념 잘알고있어야된다.
//어차피 직접 쓰레드를 써서 만드는건 많지 않고 비동기 함수 제공된걸 쓴다...ajax메소드
//아파치 톰캣 내부 다 쓰레드 들어가있는데, 우리가 직접 안만든다. 그러니까 다행.
//만약 서버자체를 직접 개발하는 서버개발자라면 쓰레드를 잘알아야된다. , 통신..
//쓰레드는 여러가지 흐름을 비동기적으로 실행하기 위한 목적
//채팅 프로그램 : 송신 기능, 수신 기능 동시에 실행되게!
//이게 당연한게 아님. 처음부터 그런걸 써서 당연하다고 느끼지만
//우리가 카카오톡 채팅,송신,수신 동시에 되는게 당연한게 아니다
//비동기적으로 송신, 수신이 비동기적으로 동작하기 떄문에..
class ThreadTest extends Thread{
	//ThreadTest 클래스가 Thread클래스를 상속받았다.
	private int num=0;
	
	public ThreadTest() {
		
	}
	public ThreadTest(int num) {
		this.num=num;
	}
	//동시 실행하고 싶은 기능
	public void run() {
		System.out.println(num+"번 쓰레드 동작중...");
		try {
			for(int i=0; i<=10; i++) {
				System.out.println(num+"번 쓰레드 동작중"+i);
				Thread.sleep(1000);
			}
		} catch(InterruptedException e) {
			e.printStackTrace();
		}
		
	}
}

public class test1 {
	public static void main(String[] args) {
		ThreadTest test1=new ThreadTest(1); // Thread클래스를 상속받은 ThreadTest 객체 생성
	 	ThreadTest test2=new ThreadTest(2);
	 	test1.start(); //run을 비동기적으로 실행
	 	test2.start(); //run을 비동기적으로 실행
	 	//이론적으로는 이 2개의 쓰레드가 비동기적으로 동시실행
	 	//그니깐 막 섞여서 실행된다.
	 	
	 	//동기적인-synchronous-바톤터치- 1번작업 다하고, 2번 작업 한다
	 	//비동기적인-asynchronous-1번 작업 하는 와중에, 2번 작업 한다.
	 	//동기적으로 하는것 장단점
	 	//일의 순서를 딱딱 지켜야되는것.. 동기적으로 해야됨
	 	//디자인-퍼블리싱...어떤 선행작업이 다 완료되어야 다음껄 한다.
	 	//비동기적으로 하는거 장단점..
	 	//채팅 : 송신, 수신 비동기적으로 실행되어야된다.
	 	//송신 다하고 수신하고 이런게 아니다.
	 	//싱크로나이즈드 스위밍-2명이 동기화해서 똑같이 일치한.. 2명이서 합을 맞춰서
	}
	
}
