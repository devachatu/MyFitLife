#include<iostream>
#include<String.h>
using namespace std;
class Node{
public:
int time; Node *next;
};
class linkedlist11{
public:
Node *temp; Node *lstptr = NULL; int length = 0;
void insert_end11(int x){
            length++;
            Node *newnode=new Node;
            newnode->time=x;
            if (lstptr==NULL)lstptr=newnode;
            else{temp=lstptr;
                while(temp->next)temp=temp->next;
                temp->next=newnode;
            }
        }
        void display11(){
            temp=lstptr; int i=1;
            cout<<"\n Choose your time slots";
            if(lstptr==NULL)cout<<"//";
            else{while(temp->next){
                cout<<i<<". "<<temp->time<<" \n";
                temp=temp->next;
                }
            }
        }
        int delete_inb11(int x){
            length--;
            Node *tmp;
            temp=lstptr;
            for(int i=x;i>2;i--){
                temp=temp->next;
            }
            tmp=temp->next;int t; t = tmp->time;
            temp->next=tmp->next;
            delete(tmp);
            return t;
        }
};
int main(){
int i;
cout<<"\n ----------------------------------------------------------------------------------------- \n";
cout<<"\n ----------------------------------------------------------------------------------------- \n";
cout<<"\n --------------------********Welcome to the Appointment Booker********-------------------- \n";
cout<<"\n ----------------------------------------------------------------------------------------- \n";
cout<<"\n ----------------------------------------------------------------------------------------- \n";
cout<<"\n Enter your City \n1. Pune \n2. Mumbai";int c;
cin>>c; linkedlist11 l11;
if(c==1){
cout<<"\n Choose your Hospital \n1. Hospital #1 \n2. Hospital #2";int h;
cin>>h;
if(h==1){
cout<<"\n Choose your doctor n1. General Physician \n2. Dermatologist";
int d; cin>>d;
for(i=2;i<=8;i++){
l11.insert_end11(i);

}int t11;
cout<<"\n Choose the doctor";
cin>>t11;

int hours = l11.delete_inb11(t11);
cout<<"\n Appointment booked at "<< hours<<":00 PM";
}

}
}