package thread_test;

import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;

public class horse extends Thread {
	int horse=0;
	Random r=new Random();
	static Scanner sc=new Scanner(System.in);
	static ArrayList<horse> pList = new ArrayList<horse>();
	
	public horse(int horse) {
		this.horse=horse;
	}
	public void run() {
		try {
			System.out.println(horse+1 +"���� ���!");
			int g = 0;
			while (true) {
				int i = 0;
				i += r.nextInt(20);
				g += i;
				if (g > 100) {
					int t = g - 100;
					g -= t;
					}
				System.out.println(horse+1 + "���� ��ǥ����" + g);
				if (g == 100) {
					System.out.println(horse+1 + "���� ����!");
					break;
				}
				Thread.sleep(200);
			}
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}
	public static void player() {
		System.out.print("�渶�� ������ �÷��̾� ���� �����ϼ��� : ");
		int sel = sc.nextInt();
		for(int i=0; i<sel; i++) {
			pList.add(new horse(i));
			pList.get(i).start();
		}
		
 	}
	
	
	public static void main(String[] args) {
//		horse h1=new horse(1);
//		horse h2=new horse(2);
		player();
	}
}
