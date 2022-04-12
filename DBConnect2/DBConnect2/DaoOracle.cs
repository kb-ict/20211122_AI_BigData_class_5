using DBConnect2.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect2
{
    class DaoOracle
    {
        static DaoOracle inst;
        string ORADB = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" +
            "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
            "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
            "User Id=test;Password=1234";
        OracleConnection conn;
        OracleCommand cmd;
        List<AddrUser> userList = new List<AddrUser>(); // 유저정보 담긴 리스트

        public static DaoOracle Instance()
        {
            if (inst == null)
            {
                inst = new DaoOracle();
            }
            return inst;
        }

        public DaoOracle() // 생성자
        {
            conn = new OracleConnection(ORADB);
            cmd = new OracleCommand();
        }

        ~DaoOracle() // 소멸자
        {
            dbClose();
        }

        public void dbConn() // 오라클 접속
        {
            try
            {
                conn.Open();
                Console.WriteLine("오라클 DB 접속 성공");
            }
            catch (OracleException ex)
            {
                Console.WriteLine("접속 에러 :" + ex.Message);
                return;
            }
        }
        public void dbClose() // 오라클 접속해제
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                    Console.WriteLine("오라클 DB 접속 해제");
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine("접속 해제 에러 :" + ex.Message);
            }
        }

        public void createTable() // 테이블 생성(testTB1)
        {
            try
            {
                string sql = "create table testTB1 (" +
                    "id number not null," +
                    "name varchar2(20) not null," +
                    "age number not null," +
                    "addr varchar2(80) not null," +
                    "constraint pk_testTB1_id primary key(id))";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Console.WriteLine("테이블 생성 성공!");

                string sql2 = "create sequence seq_id increment " +
                    "by 1 start with 1"; // 1씩 자동증가(=오토 인크리먼트) 고유한 키 값
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                Console.WriteLine("시퀀스 생성 성공!"); // 생성순서 : 테이블 -> 시퀀스(삭제는 반대)
            }
            catch (OracleException ex)
            {
                Console.WriteLine("테이블(시퀀스) 생성 에러: " + ex.Message);
            }
        }

        public void dropTable() // 테이블 삭제(testTB1)
        {
            try
            {
                string sql = "drop table testTB1";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Console.WriteLine("테이블 삭제 성공!");

                string sql2 = "drop sequence seq_id";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                Console.WriteLine("시퀀스 삭제 성공!");

            }
            catch (OracleException ex)
            {
                Console.WriteLine("테이블(시퀀스) 삭제 에러: " + ex.Message);
            }
        }

        public void insertDB() // (샘플) 테이터 추가
        {
            try
            {
                string name = "홍길동";
                int age = 300;
                string addr = "조선 한양 11번지";
                string sql = string.Format($"insert into testTB1 values (seq_id.nextval,'{name}', {age}, '{addr}')");
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Console.WriteLine(name + " 데이터 추가 성공!");
            }
            catch (OracleException ex)
            {
                Console.WriteLine("데이터 추가 에러: " + ex.Message);
            }
        }

        public void insertDB(AddrUser user) // 데이터 추가
        {
            try
            {
                string name = user.Name;
                int age = user.Age;
                string addr = user.Addr;
                string sql = string.Format($"insert into testTB1 values (seq_id.nextval,'{name}', {age}, '{addr}')");
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Console.WriteLine("데이터 추가 에러: " + ex.Message);
            }
        }

        public void showDB() // 데이터 보기
        {
            try
            {
                string sql = "select * from testTB1 order by id asc";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read()) // 행 1개 읽기
                    {
                        Console.WriteLine("ID: " + reader["id"]);
                        Console.WriteLine("이름: " + reader["name"]);
                        Console.WriteLine("나이: " + reader["age"]);
                        Console.WriteLine("주소: " + reader["addr"]);
                        Console.WriteLine("-----------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("데이터가 존재하지 않습니다.");
                }
                reader.Close();
            }
            catch (OracleException ex)
            {
                Console.WriteLine("데이터 추가 에러: " + ex.Message);
            }
        }

        public List<AddrUser> getDB() // 데이터 가져오기
        {
            try
            {
                string sql = "select * from testTB1 order by id";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                OracleDataReader reader = cmd.ExecuteReader();
                userList.Clear(); // 담기전에 초기화

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // 가져온 정보를 담기
                        userList.Add(new AddrUser(
                            int.Parse(reader["id"].ToString()),
                            reader["name"].ToString(),
                            int.Parse(reader["age"].ToString()),
                            reader["addr"].ToString()));
                    }
                }
                else
                {
                    Console.WriteLine("데이터가 존재하지 않습니다.");
                }
                reader.Close();
            }
            catch (OracleException ex)
            {
                Console.WriteLine("데이터 보기 에러: " + ex.Message);
            }
            return userList;
        }

        public void updateDB() // (샘플)데이터 수정
        {
            try
            {
                string sql = "update testTB1 set name='전우치' where name='홍길동'";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Console.WriteLine("데이터 수정 성공!");
            }
            catch (OracleException ex)
            {
                Console.WriteLine("데이터 수정 에러: " + ex.Message);
            }
        }
        public void updateDB(int id, string name, int age, string addr) // 데이터 수정
        {
            try
            {
                string sql = string.Format($"update testTB1 set name='{name}', age={age}, addr='{addr}' where id={id}");
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Console.WriteLine("데이터 수정 성공!");
            }
            catch (OracleException ex)
            {
                Console.WriteLine("데이터 수정 에러: " + ex.Message);
            }
        }
        public void deleteDB(int id) // 데이터 삭제
        {
            try
            {
                string sql = string.Format($"delete from testTB1 where id={id}");
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Console.WriteLine("데이터 삭제 성공!");
            }
            catch (OracleException ex)
            {
                Console.WriteLine("데이터 삭제 에러: " + ex.Message);
            }
        }

        

    }//클래스
}//메인
