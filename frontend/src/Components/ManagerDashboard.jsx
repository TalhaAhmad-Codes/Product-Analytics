import '../index.css';
import React, { useState, useEffect } from 'react';

const ManagerDashboard = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isEditing, setIsEditing] = useState(false);
  const [editingProduct, setEditingProduct] = useState(null);

  // 1. READ: Fetch data from your API
  useEffect(() => {
    const fetchProducts = async () => {
      try {
        setLoading(true);
        // Replace with your actual endpoint: e.g., fetch('https://api.yourdomain.com/products')
        const response = await fetch('/api/products');
        if (!response.ok) throw new Error('Failed to fetch inventory');
        const data = await response.json();
        setProducts(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  // 2. DELETE: Function to call delete API
  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this product?")) {
      try {
        const response = await fetch(`/api/products/${id}`, { method: 'DELETE' });
        if (!response.ok) throw new Error('Failed to delete product');
        // Update UI locally after successful delete
        setProducts(products.filter(p => p.id !== id));
      } catch (err) {
        alert("Error deleting product");
      }
    }
  };

  // 3. EDIT: Function to open edit modal
  const handleEdit = (product) => {
    setEditingProduct(product);
    setIsEditing(true);``
  };

  // 4. UPDATE: Function to call update API
  const handleUpdate = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`/api/products/${editingProduct.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(editingProduct),
      });
      if (!response.ok) throw new Error('Failed to update product');
      
      // Update UI locally after successful update
      setProducts(products.map(p => p.id === editingProduct.id ? editingProduct : p));
      setIsEditing(false);
      setEditingProduct(null);
    } catch (err) {
      alert("Error updating product");
    }
  };

  return (
    <div className="min-h-screen bg-slate-50 font-sans text-slate-900">
      {/* Top Header */}
      <header className="bg-white border-b border-slate-200 px-8 py-4 flex justify-between items-center sticky top-0 z-10">
        <div className="flex items-center gap-4">
          <div className="bg-[#1a2b4b] p-2 rounded text-white font-bold tracking-tighter">MP</div>
          <h1 className="text-xl font-bold uppercase tracking-tight text-slate-700">Manage Products</h1>
        </div>
        <div className="flex items-center gap-3 border-l pl-6 border-slate-200">
          <div className="text-right">
            <p className="text-sm font-bold leading-none">Sarah Jenkins</p>
            <p className="text-[10px] text-slate-500 font-mono">MGR-ID: 251611337</p>
          </div>
          <div className="w-9 h-9 bg-slate-200 rounded-full border border-slate-300"></div>
        </div>
      </header>

      <main className="p-8 max-w-7xl mx-auto">
        <div className="bg-white rounded-lg shadow-sm border border-slate-200 overflow-hidden">

          {/* Table Controls */}
          <div className="p-5 border-b border-slate-100 flex justify-between items-center bg-slate-50/50">
            <div className="flex gap-3">
              <input
                type="text"
                placeholder="Search inventory..."
                className="border border-slate-300 rounded px-4 py-2 text-sm w-72 focus:ring-2 focus:ring-blue-500 outline-none transition-all"
              />
            </div>
            <button className="bg-[#1e88e5] hover:bg-blue-600 text-white px-5 py-2 rounded-md font-bold text-sm transition-colors shadow-sm active:transform active:scale-95">
              + ADD NEW PRODUCT
            </button>
          </div>

          {/* Table State Handling */}
          {loading ? (
            <div className="p-20 text-center text-slate-400 italic">Loading inventory data...</div>
          ) : error ? (
            <div className="p-20 text-center text-red-500">Error: {error}</div>
          ) : (
            <table className="w-full text-left border-collapse">
              <thead>
                <tr className="bg-slate-100/50 text-slate-500 text-[11px] uppercase tracking-wider font-semibold border-b border-slate-200">
                  <th className="px-6 py-4">Product Details</th>
                  <th className="px-6 py-4">SKU Code</th>
                  <th className="px-6 py-4 text-center">Price</th>
                  <th className="px-6 py-4 text-center">Stock</th>
                  <th className="px-6 py-4">Status</th>
                  <th className="px-6 py-4 text-right">Actions</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-slate-100">
                {products.length === 0 ? (
                  <tr><td colSpan="6" className="p-10 text-center text-slate-400">No products found in database.</td></tr>
                ) : (
                  products.map((product) => (
                    <tr key={product.id} className="hover:bg-blue-50/20 transition-colors group">
                      <td className="px-6 py-4 font-semibold text-slate-800">{product.name}</td>
                      <td className="px-6 py-4 text-slate-500 font-mono text-sm">{product.sku}</td>
                      <td className="px-6 py-4 text-center font-bold text-slate-700">${product.price}</td>
                      <td className="px-6 py-4 text-center">{product.stock}</td>
                      <td className="px-6 py-4">
                        <span className={`px-2.5 py-1 rounded text-[10px] font-bold tracking-wide border ${
                          product.stock > 10
                            ? 'bg-green-50 text-green-700 border-green-200'
                            : 'bg-orange-50 text-orange-700 border-orange-200'
                        }`}>
                          {product.stock > 10 ? 'IN STOCK' : 'LOW STOCK'}
                        </span>
                      </td>
                      <td className="px-6 py-4 text-right">
                        <div className="flex justify-end gap-2 opacity-0 group-hover:opacity-100 transition-opacity">
                          <button 
                            onClick={() => handleEdit(product)}
                            className="p-2 bg-blue-100 text-blue-600 rounded hover:bg-blue-600 hover:text-white transition-all"
                          >
                            <span className="text-xs">EDIT</span>
                          </button>
                          <button
                            onClick={() => handleDelete(product.id)}
                            className="p-2 bg-red-100 text-red-600 rounded hover:bg-red-600 hover:text-white transition-all"
                          >
                            <span className="text-xs">DEL</span>
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          )}
        </div>
      </main>

      {/* Edit Modal */}
      {isEditing && editingProduct && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-white rounded-lg p-6 w-full max-w-md mx-4">
            <h2 className="text-xl font-bold mb-4">Edit Product</h2>
            <form onSubmit={handleUpdate}>
              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700 mb-1">Product Name</label>
                <input
                  type="text"
                  value={editingProduct.name}
                  onChange={(e) => setEditingProduct({...editingProduct, name: e.target.value})}
                  className="w-full border border-gray-300 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 outline-none"
                  required
                />
              </div>
              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700 mb-1">SKU Code</label>
                <input
                  type="text"
                  value={editingProduct.sku}
                  onChange={(e) => setEditingProduct({...editingProduct, sku: e.target.value})}
                  className="w-full border border-gray-300 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 outline-none"
                  required
                />
              </div>
              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700 mb-1">Price</label>
                <input
                  type="number"
                  step="0.01"
                  value={editingProduct.price}
                  onChange={(e) => setEditingProduct({...editingProduct, price: parseFloat(e.target.value)})}
                  className="w-full border border-gray-300 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 outline-none"
                  required
                />
              </div>
              <div className="mb-6">
                <label className="block text-sm font-medium text-gray-700 mb-1">Stock</label>
                <input
                  type="number"
                  value={editingProduct.stock}
                  onChange={(e) => setEditingProduct({...editingProduct, stock: parseInt(e.target.value)})}
                  className="w-full border border-gray-300 rounded px-3 py-2 focus:ring-2 focus:ring-blue-500 outline-none"
                  required
                />
              </div>
              <div className="flex justify-end gap-3">
                <button
                  type="button"
                  onClick={() => {
                    setIsEditing(false);
                    setEditingProduct(null);
                  }}
                  className="px-4 py-2 text-gray-600 border border-gray-300 rounded hover:bg-gray-50"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                >
                  Update Product
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default ManagerDashboard;