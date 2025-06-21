#include <cpp/unisym>
#include <cpp/nodes>
#include <cpp/bnode>
#include <c/consio.h>

using namespace uni;

bool procfunc(Bnode* nod) {
	if (nod) printf(" %s ", nod->addr);
	return true;
}

/*
	1
   /  \
  2    3
 / \    \
4   5    6
	 \
	  7
*/

void Tra_inorder(Bnode* root) {
	if (!root) return;
	Dchain dc(nullptr);
	Bnode* crt = root;
	while (true) {
		if (crt) {
			dc.Append(crt); crt = crt->left;// to bottom
		}
		else if (dc.Count()) {
			crt = (Bnode*)dc.Last()->offs;
			dc.Remove(dc.Last());
			procfunc(crt);
			crt = crt->next;
		}
		else break;
	}
}

void Tra_postorder(Bnode* root) {

}

int main() {
	Bnode root;
	root.addr = (char*)"a1";
	Bnode n2, n3, n4, n5, n6, n7;
	n2.addr = (char*)"a2";
	n3.addr = (char*)"a3";
	n4.addr = (char*)"a4";
	n5.addr = (char*)"a5";
	n6.addr = (char*)"a6";
	n7.addr = (char*)"a7";
	root.left = &n2;
	root.next = &n3;
	n2.left = &n4;
	n2.next = &n5;
	n3.next = &n6;
	n5.next = &n7;
	Console.OutFormat("层序遍历\n");
	BnodeChain::Traversal(BnodeChain::TraversalOrder::Levelorder, procfunc, &root);
	Console.OutFormat("\n");
	Console.OutFormat("前序遍历\n");
	BnodeChain::Traversal(BnodeChain::TraversalOrder::Preorder, procfunc, &root);
	Console.OutFormat("\n");
	Console.OutFormat("中序遍历\n");
	BnodeChain::Traversal(BnodeChain::TraversalOrder::Inorder, procfunc, &root);
	Console.OutFormat("\n");
	Tra_inorder(&root);
	Console.OutFormat("\n");
	Console.OutFormat("后序遍历\n");
	BnodeChain::Traversal(BnodeChain::TraversalOrder::Postorder, procfunc, &root);
	Console.OutFormat("\n");

	return malc_count;
}

